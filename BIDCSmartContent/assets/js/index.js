$("#e1").select2({
  placeholder: "Users input",
  allowClear: true,
  
  // match strings that begins with (instead of contains):
  matcher: function(term, text) {
        return text.toUpperCase().indexOf(term.toUpperCase())==0;
  }
});