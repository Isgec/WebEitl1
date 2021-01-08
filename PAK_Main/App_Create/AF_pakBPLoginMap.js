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
    validate_LoginID: function(sender) {
      var Prefix = sender.id.replace('LoginID','');
      this.validated_FK_SYS_BPLoginMap_LoginID_main = true;
      this.validate_FK_SYS_BPLoginMap_LoginID(sender,Prefix);
      this.validatePK_pakBPLoginMap(sender,Prefix);
      },
    validate_FK_SYS_BPLoginMap_LoginID: function(o,Prefix) {
      var value = o.id;
      var LoginID = $get(Prefix + 'LoginID');
      if(LoginID.value==''){
        if(this.validated_FK_SYS_BPLoginMap_LoginID_main){
          var o_d = $get(Prefix + 'LoginID' + '_Display');
          try{o_d.innerHTML = '';}catch(ex){}
        }
        return true;
      }
      value = value + ',' + LoginID.value ;
        o.style.backgroundImage  = 'url(../../images/pkloader.gif)';
        o.style.backgroundRepeat= 'no-repeat';
        o.style.backgroundPosition = 'right';
        PageMethods.validate_FK_SYS_BPLoginMap_LoginID(value, this.validated_FK_SYS_BPLoginMap_LoginID);
      },
    validated_FK_SYS_BPLoginMap_LoginID_main: false,
    validated_FK_SYS_BPLoginMap_LoginID: function(result) {
      var p = result.split('|');
      var o = $get(p[1]);
      if(script_pakBPLoginMap.validated_FK_SYS_BPLoginMap_LoginID_main){
        var o_d = $get(p[1]+'_Display');
        try{o_d.innerHTML = p[2];}catch(ex){}
      }
      o.style.backgroundImage  = 'none';
      if(p[0]=='1'){
        o.value='';
        o.focus();
      }
    },
    validatePK_pakBPLoginMap: function(o,Prefix) {
      var value = o.id;
      try{$get(Prefix.replace('_F_','') + '_L_ErrMsgpakBPLoginMap').innerHTML = '';}catch(ex){}
      var LoginID = $get(Prefix + 'LoginID');
      if(LoginID.value=='')
        return true;
      value = value + ',' + LoginID.value ;
      var BPID = $get(Prefix + 'BPID');
      if(BPID.value=='')
        return true;
      value = value + ',' + BPID.value ;
      var Comp = $get(Prefix + 'Comp');
      if(Comp.value=='')
        return true;
      value = value + ',' + Comp.value ;
      o.style.backgroundImage  = 'url(../../images/pkloader.gif)';
      o.style.backgroundRepeat= 'no-repeat';
      o.style.backgroundPosition = 'right';
      PageMethods.validatePK_pakBPLoginMap(value, this.validatedPK_pakBPLoginMap);
    },
    validatedPK_pakBPLoginMap: function(result) {
      var p = result.split('|');
      var o = $get(p[1]);
      o.style.backgroundImage  = 'none';
      if(p[0]=='1'){
        try{$get('cph1_FVpakBPLoginMap_L_ErrMsgpakBPLoginMap').innerHTML = p[2];}catch(ex){}
        o.value='';
        o.focus();
      }
    },
    temp: function() {
    }
    }
</script>
