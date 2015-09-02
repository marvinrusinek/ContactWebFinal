(function() {
  this.ContactSearch = (function() {
    function ContactSearch(id, url) {
      $(function() {
        return $(id).autocomplete({
          source: function(req, res) {
            return console.log(req);
          }
        });
      });
    }
    return ContactSearch;
  })();
}).call(this);
