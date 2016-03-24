using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.AspNet.Mvc.ViewEngines;
using Microsoft.Extensions.OptionsModel;

namespace ZKCloud.Web.Mvc {
    public class AppRazorViewEngine : IRazorViewEngine {
        private const string ViewExtension = ".cshtml";
        internal const string ControllerKey = "controller";
        internal const string AppKey = "app";

        private static readonly IEnumerable<string> _viewLocationFormats = new[]
        {
            "/views/{1}/{0}" + ViewExtension,
            "/views/shared/{0}" + ViewExtension,
        };

        private static readonly IEnumerable<string> _appViewLocationFormats = new[]
        {
            "/apps/{2}/template/html/{1}_{0}" + ViewExtension,
			"/apps/{2}/template/page/{1}_{0}" + ViewExtension,
			"/apps/{2}/template/widget/{0}" + ViewExtension,
            "/views/shared/{0}" + ViewExtension,
        };

        private readonly IRazorPageFactory _pageFactory;
        private readonly IRazorViewFactory _viewFactory;
        private readonly IList<IViewLocationExpander> _viewLocationExpanders;
        private readonly IViewLocationCache _viewLocationCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="RazorViewEngine" /> class.
        /// </summary>
        /// <param name="pageFactory">The page factory used for creating <see cref="IRazorPage"/> instances.</param>
        public AppRazorViewEngine(
            IRazorPageFactory pageFactory,
            IRazorViewFactory viewFactory,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            IViewLocationCache viewLocationCache) {
            _pageFactory = pageFactory;
            _viewFactory = viewFactory;
            _viewLocationExpanders = optionsAccessor.Value.ViewLocationExpanders;
            _viewLocationCache = viewLocationCache;
        }

        /// <summary>
        /// Gets the locations where this instance of <see cref="RazorViewEngine"/> will search for views.
        /// </summary>
        /// <remarks>
        /// The locations of the views returned from controllers that do not belong to an app.
        /// Locations are composite format strings (see http://msdn.microsoft.com/en-us/library/txafckwd.aspx),
        /// which contains following indexes:
        /// {0} - Action Name
        /// {1} - Controller Name
        /// The values for these locations are case-sensitive on case-senstive file systems.
        /// For example, the view for the <c>Test</c> action of <c>HomeController</c> should be located at
        /// <c>/Views/Home/Test.cshtml</c>. Locations such as <c>/views/home/test.cshtml</c> would not be discovered
        /// </remarks>
        public virtual IEnumerable<string> ViewLocationFormats {
            get { return _viewLocationFormats; }
        }

        /// <summary>
        /// Gets the locations where this instance of <see cref="RazorViewEngine"/> will search for views within an
        /// app.
        /// </summary>
        /// <remarks>
        /// The locations of the views returned from controllers that belong to an app.
        /// Locations are composite format strings (see http://msdn.microsoft.com/en-us/library/txafckwd.aspx),
        /// which contains following indexes:
        /// {0} - Action Name
        /// {1} - Controller Name
        /// {2} - App name
        /// The values for these locations are case-sensitive on case-senstive file systems.
        /// For example, the view for the <c>Test</c> action of <c>HomeController</c> should be located at
        /// <c>/Views/Home/Test.cshtml</c>. Locations such as <c>/views/home/test.cshtml</c> would not be discovered
        /// </remarks>
        public virtual IEnumerable<string> AppViewLocationFormats {
            get { return _appViewLocationFormats; }
        }

        /// <inheritdoc />
        public ViewEngineResult FindView(
            ActionContext context,
            string viewName) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(viewName)) {
                throw new ArgumentException($"{nameof(viewName)} can not be null or empty.");
            }

            var pageResult = GetRazorPageResult(context, viewName, isPartial: false);
            return CreateViewEngineResult(pageResult, _viewFactory, isPartial: false);
        }

        /// <inheritdoc />
        public ViewEngineResult FindPartialView(
            ActionContext context,
            string partialViewName) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(partialViewName)) {
                throw new ArgumentException($"{nameof(partialViewName)} can not be null or empty.");
            }

            var pageResult = GetRazorPageResult(context, partialViewName, isPartial: true);
            return CreateViewEngineResult(pageResult, _viewFactory, isPartial: true);
        }

        /// <inheritdoc />
        public RazorPageResult FindPage(
            ActionContext context,
            string pageName) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(pageName)) {
                throw new ArgumentException($"{nameof(pageName)} can not be null or empty.");
            }

            return GetRazorPageResult(context, pageName, isPartial: true);
        }

        /// <summary>
        /// Gets the case-normalized route value for the specified route <paramref name="key"/>.
        /// </summary>
        /// <param name="context">The <see cref="ActionContext"/>.</param>
        /// <param name="key">The route key to lookup.</param>
        /// <returns>The value corresponding to the key.</returns>
        /// <remarks>
        /// The casing of a route value in <see cref="ActionContext.RouteData"/> is determined by the client.
        /// This making constructing paths for view locations in a case sensitive file system unreliable. Using the
        /// <see cref="Abstractions.ActionDescriptor.RouteValueDefaults"/> for attribute routes and
        /// <see cref="Abstractions.ActionDescriptor.RouteConstraints"/> for traditional routes to get route values
        /// produces consistently cased results.
        /// </remarks>
        public static string GetNormalizedRouteValue(
            ActionContext context,
            string key) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (key == null) {
                throw new ArgumentNullException(nameof(key));
            }

            object routeValue;
            if (!context.RouteData.Values.TryGetValue(key, out routeValue)) {
                return null;
            }

            var actionDescriptor = context.ActionDescriptor;
            string normalizedValue = null;
            if (actionDescriptor.AttributeRouteInfo != null) {
                object match;
                if (actionDescriptor.RouteValueDefaults.TryGetValue(key, out match)) {
                    normalizedValue = match?.ToString();
                }
            } else {
                // Perf: Avoid allocations
                for (var i = 0; i < actionDescriptor.RouteConstraints.Count; i++) {
                    var constraint = actionDescriptor.RouteConstraints[i];
                    if (string.Equals(constraint.RouteKey, key, StringComparison.Ordinal)) {
                        if (constraint.KeyHandling == RouteKeyHandling.DenyKey) {
                            return null;
                        } else if (constraint.KeyHandling == RouteKeyHandling.RequireKey) {
                            normalizedValue = constraint.RouteValue;
                        }

                        // Duplicate keys in RouteConstraints are not allowed.
                        break;
                    }
                }
            }

            var stringRouteValue = routeValue?.ToString();
            if (string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase)) {
                return normalizedValue;
            }

            return stringRouteValue;
        }

        private RazorPageResult GetRazorPageResult(
            ActionContext context,
            string pageName,
            bool isPartial) {
            if (IsApplicationRelativePath(pageName)) {
                var applicationRelativePath = pageName;
                if (!pageName.EndsWith(ViewExtension, StringComparison.OrdinalIgnoreCase)) {
                    applicationRelativePath += ViewExtension;
                }

                var page = _pageFactory.CreateInstance(applicationRelativePath);
                if (page != null) {
                    return new RazorPageResult(pageName, page);
                }

                return new RazorPageResult(pageName, new[] { pageName });
            } else {
                return LocatePageFromViewLocations(context, pageName, isPartial);
            }
        }

        private RazorPageResult LocatePageFromViewLocations(
            ActionContext context,
            string pageName,
            bool isPartial) {
            // Initialize the dictionary for the typical case of having controller and action tokens.
            var appName = GetNormalizedRouteValue(context, AppKey);

            // Only use the app view location formats if we have an app token.
            var viewLocations = !string.IsNullOrEmpty(appName) ? AppViewLocationFormats :
                                                                  ViewLocationFormats;

            var expanderContext = new ViewLocationExpanderContext(context, pageName, isPartial);
            if (_viewLocationExpanders.Count > 0) {
                expanderContext.Values = new Dictionary<string, string>(StringComparer.Ordinal);

                // 1. Populate values from viewLocationExpanders.
                // Perf: Avoid allocations
                for (var i = 0; i < _viewLocationExpanders.Count; i++) {
                    _viewLocationExpanders[i].PopulateValues(expanderContext);
                }
            }

            // 2. With the values that we've accumumlated so far, check if we have a cached result.
            IEnumerable<string> locationsToSearch = null;
            var cachedResult = _viewLocationCache.Get(expanderContext);
            if (!cachedResult.Equals(ViewLocationCacheResult.None)) {
                if (cachedResult.IsFoundResult) {
                    var page = _pageFactory.CreateInstance(cachedResult.ViewLocation);

                    if (page != null) {
                        // 2a We have a cache entry where a view was previously found.
                        return new RazorPageResult(pageName, page);
                    }
                } else {
                    locationsToSearch = cachedResult.SearchedLocations;
                }
            }

            if (locationsToSearch == null) {
                // 2b. We did not find a cached location or did not find a IRazorPage at the cached location.
                // The cached value has expired and we need to look up the page.
                foreach (var expander in _viewLocationExpanders) {
                    viewLocations = expander.ExpandViewLocations(expanderContext, viewLocations);
                }

                var controllerName = GetNormalizedRouteValue(context, ControllerKey);

                locationsToSearch = viewLocations.Select(
                    location => string.Format(
                        CultureInfo.InvariantCulture,
                        location,
                        pageName,
                        controllerName,
                        appName
                    ));
            }

            // 3. Use the expanded locations to look up a page.
            var searchedLocations = new List<string>();
            foreach (var path in locationsToSearch) {
                var page = _pageFactory.CreateInstance(path);
                if (page != null) {
                    // 3a. We found a page. Cache the set of values that produced it and return a found result.
                    _viewLocationCache.Set(expanderContext, new ViewLocationCacheResult(path, searchedLocations));
                    return new RazorPageResult(pageName, page);
                }

                searchedLocations.Add(path);
            }

            // 3b. We did not find a page for any of the paths.
            _viewLocationCache.Set(expanderContext, new ViewLocationCacheResult(searchedLocations));
            return new RazorPageResult(pageName, searchedLocations);
        }

        private ViewEngineResult CreateViewEngineResult(
            RazorPageResult result,
            IRazorViewFactory razorViewFactory,
            bool isPartial) {
            if (result.SearchedLocations != null) {
                return ViewEngineResult.NotFound(result.Name, result.SearchedLocations);
            }

            var view = razorViewFactory.GetView(this, result.Page, isPartial);
            return ViewEngineResult.Found(result.Name, view);
        }

        private static bool IsApplicationRelativePath(string name) {
            Debug.Assert(!string.IsNullOrEmpty(name));
            return name[0] == '~' || name[0] == '/';
        }
    }
}
