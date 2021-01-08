<script type="text/javascript"> 
var script_pakBPLoginMap = {
    ACELoginID_Selected: function(sender, e) {
      var Prefix = sender._element.id.replace('LoginID','');
      var F_LoginID = $get(sender._element.id);
      var F_LoginID_Display = $get(sender._element.id + '_Display');
      var retval = e.get_value();
      var p = retval.split('|');
      F_LoginID_Display.innerHTML = e.get_text();
    },
    ACELoginID_Populating: function(sender,e) {
      var p = sender.get_element();
      var Prefix = sender._element.id.replace('LoginID','');
      p.style.backgroundImage  = 'url(../../images/loader.gif)';
      p.style.backgroundRepeat= 'no-repeat';
      p.style.backgroundPosition = 'right';
      sender._contextKey = '';
    },
    ACELoginID_Populated: function(sender,e) {
      var p = sender.get_element();
      p.style.backgroundImage  = 'none';
    },
    temp: function() {
    }
    }
</script>
