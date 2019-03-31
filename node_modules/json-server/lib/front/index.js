"use strict";

require("promise-polyfill/src/polyfill");

require("whatwg-fetch");

var _preact = require("preact");

require("milligram/dist/milligram.css");

require("./style.css");

function ResourceItem(_ref) {
  var name = _ref.name,
      length = _ref.length;
  return (0, _preact.h)("li", null, (0, _preact.h)("a", {
    href: name
  }, "/", name), " ", (0, _preact.h)("sup", null, length ? "".concat(length, "x") : 'object'));
}

function ResourceList(_ref2) {
  var db = _ref2.db;
  return (0, _preact.h)("ul", null, Object.keys(db).map(function (name) {
    return (0, _preact.h)(ResourceItem, {
      name: name,
      length: Array.isArray(db[name]) && db[name].length
    });
  }));
}

function NoResources() {
  return (0, _preact.h)("p", null, "No resources found");
}

function ResourcesBlock(_ref3) {
  var db = _ref3.db;
  return (0, _preact.h)("div", null, (0, _preact.h)("h4", null, "Resources"), Object.keys(db).length ? (0, _preact.h)(ResourceList, {
    db: db
  }) : (0, _preact.h)(NoResources, null));
}

window.fetch('db').then(function (response) {
  return response.json();
}).then(function (db) {
  return (0, _preact.render)((0, _preact.h)(ResourcesBlock, {
    db: db
  }), document.getElementById('resources'));
});

function CustomRoutesBlock(_ref4) {
  var customRoutes = _ref4.customRoutes;
  var rules = Object.keys(customRoutes);

  if (rules.length) {
    return (0, _preact.h)("div", null, (0, _preact.h)("h4", null, "Custom Routes"), (0, _preact.h)("table", null, rules.map(function (rule) {
      return (0, _preact.h)("tr", null, (0, _preact.h)("td", null, rule), (0, _preact.h)("td", null, "\u21E2 ", customRoutes[rule]));
    })));
  }
}

window.fetch('__rules').then(function (response) {
  return response.json();
}).then(function (customRoutes) {
  (0, _preact.render)((0, _preact.h)(CustomRoutesBlock, {
    customRoutes: customRoutes
  }), document.getElementById('custom-routes'));
});