(function() {
  var __indexOf = Array.prototype.indexOf || function(item) {
    for (var i = 0, l = this.length; i < l; i++) {
      if (this[i] === item) return i;
    }
    return -1;
  };
  $(function() {
    var Helper, h;
    Helper = (function() {
      function Helper() {}
      Helper.prototype.form = function() {
        return $(this).parents("form");
      };
      return Helper;
    })();
    h = new Helper;
    $("h3.allFilters").click(function() {
      var form;
      form = $(this).parents("form");
      return $("input:checkbox", form).removeAttr("checked");
    });
    $("h3.filterHeader").click(function() {
      var container, form;
      form = $(this).parents("form");
      container = $(this).parents("div.filterContainer");
      $("input:checkbox", container).removeAttr("checked");
      return form.submit();
    });
    return $("input.filter").click(function() {
      var checked, container, form, name, notChecked;
      form = $(this).parents("form");
      name = $(this).attr("name");
      container = $(this).parents("div.filterContainer");
      checked = $("input[name=" + name + "]:checked", container);
      (function(i) {
        var _ref;
        return _ref = $(this).attr(name, "" + name + "[" + i + "]"), __indexOf.call(checked, _ref) >= 0;
      });
      notChecked = $("input[name=" + name + "]:not(:checked)", container);
      (function(i) {
        var _ref;
        return _ref = $(this).removeAttr("name"), __indexOf.call(notChecked, _ref) >= 0;
      });
      return form.submit();
    });
  });
}).call(this);
