<script type="text/javascript"> 
var script_pakSTCPOLRD = {
    ACESerialNo_Selected: function(sender, e) {
      var Prefix = sender._element.id.replace('SerialNo','');
      var F_SerialNo = $get(sender._element.id);
      var F_SerialNo_Display = $get(sender._element.id + '_Display');
      var retval = e.get_value();
      var p = retval.split('|');
      F_SerialNo.value = p[0];
      F_SerialNo_Display.innerHTML = e.get_text();
    },
    ACESerialNo_Populating: function(sender,e) {
      var p = sender.get_element();
      var Prefix = sender._element.id.replace('SerialNo','');
      p.style.backgroundImage  = 'url(../../images/loader.gif)';
      p.style.backgroundRepeat= 'no-repeat';
      p.style.backgroundPosition = 'right';
      sender._contextKey = '';
    },
    ACESerialNo_Populated: function(sender,e) {
      var p = sender.get_element();
      p.style.backgroundImage  = 'none';
    },
    ACEItemNo_Selected: function(sender, e) {
      var Prefix = sender._element.id.replace('ItemNo','');
      var F_ItemNo = $get(sender._element.id);
      var F_ItemNo_Display = $get(sender._element.id + '_Display');
      var retval = e.get_value();
      var p = retval.split('|');
      F_ItemNo.value = p[1];
      F_ItemNo_Display.innerHTML = e.get_text();
    },
    ACEItemNo_Populating: function(sender,e) {
      var p = sender.get_element();
      var Prefix = sender._element.id.replace('ItemNo','');
      p.style.backgroundImage  = 'url(../../images/loader.gif)';
      p.style.backgroundRepeat= 'no-repeat';
      p.style.backgroundPosition = 'right';
      sender._contextKey = '';
    },
    ACEItemNo_Populated: function(sender,e) {
      var p = sender.get_element();
      p.style.backgroundImage  = 'none';
    },
    ACEUploadNo_Selected: function(sender, e) {
      var Prefix = sender._element.id.replace('UploadNo','');
      var F_UploadNo = $get(sender._element.id);
      var F_UploadNo_Display = $get(sender._element.id + '_Display');
      var retval = e.get_value();
      var p = retval.split('|');
      F_UploadNo.value = p[2];
      F_UploadNo_Display.innerHTML = e.get_text();
    },
    ACEUploadNo_Populating: function(sender,e) {
      var p = sender.get_element();
      var Prefix = sender._element.id.replace('UploadNo','');
      p.style.backgroundImage  = 'url(../../images/loader.gif)';
      p.style.backgroundRepeat= 'no-repeat';
      p.style.backgroundPosition = 'right';
      sender._contextKey = '';
    },
    ACEUploadNo_Populated: function(sender,e) {
      var p = sender.get_element();
      p.style.backgroundImage  = 'none';
    },
    validate_SerialNo: function(sender) {
      var Prefix = sender.id.replace('SerialNo','');
      this.validated_FK_PAK_POLineRecDoc_SerialNo_main = true;
      this.validate_FK_PAK_POLineRecDoc_SerialNo(sender,Prefix);
      },
    validate_ItemNo: function(sender) {
      var Prefix = sender.id.replace('ItemNo','');
      this.validated_FK_PAK_POLineRecDoc_ItemNo_main = true;
      this.validate_FK_PAK_POLineRecDoc_ItemNo(sender,Prefix);
      },
    validate_UploadNo: function(sender) {
      var Prefix = sender.id.replace('UploadNo','');
      this.validated_FK_PAK_POLineRecDoc_UploadNo_main = true;
      this.validate_FK_PAK_POLineRecDoc_UploadNo(sender,Prefix);
      },
    validate_FK_PAK_POLineRecDoc_SerialNo: function(o,Prefix) {
      var value = o.id;
      var SerialNo = $get(Prefix + 'SerialNo');
      if(SerialNo.value==''){
        if(this.validated_FK_PAK_POLineRecDoc_SerialNo_main){
          var o_d = $get(Prefix + 'SerialNo' + '_Display');
          try{o_d.innerHTML = '';}catch(ex){}
        }
        return true;
      }
      value = value + ',' + SerialNo.value ;
        o.style.backgroundImage  = 'url(../../images/pkloader.gif)';
        o.style.backgroundRepeat= 'no-repeat';
        o.style.backgroundPosition = 'right';
        PageMethods.validate_FK_PAK_POLineRecDoc_SerialNo(value, this.validated_FK_PAK_POLineRecDoc_SerialNo);
      },
    validated_FK_PAK_POLineRecDoc_SerialNo_main: false,
    validated_FK_PAK_POLineRecDoc_SerialNo: function(result) {
      var p = result.split('|');
      var o = $get(p[1]);
      if(script_pakSTCPOLRD.validated_FK_PAK_POLineRecDoc_SerialNo_main){
        var o_d = $get(p[1]+'_Display');
        try{o_d.innerHTML = p[2];}catch(ex){}
      }
      o.style.backgroundImage  = 'none';
      if(p[0]=='1'){
        o.value='';
        o.focus();
      }
    },
    validate_FK_PAK_POLineRecDoc_ItemNo: function(o,Prefix) {
      var value = o.id;
      var SerialNo = $get(Prefix + 'SerialNo');
      if(SerialNo.value==''){
        if(this.validated_FK_PAK_POLineRecDoc_ItemNo_main){
          var o_d = $get(Prefix + 'SerialNo' + '_Display');
          try{o_d.innerHTML = '';}catch(ex){}
        }
        return true;
      }
      value = value + ',' + SerialNo.value ;
      var ItemNo = $get(Prefix + 'ItemNo');
      if(ItemNo.value==''){
        if(this.validated_FK_PAK_POLineRecDoc_ItemNo_main){
          var o_d = $get(Prefix + 'ItemNo' + '_Display');
          try{o_d.innerHTML = '';}catch(ex){}
        }
        return true;
      }
      value = value + ',' + ItemNo.value ;
        o.style.backgroundImage  = 'url(../../images/pkloader.gif)';
        o.style.backgroundRepeat= 'no-repeat';
        o.style.backgroundPosition = 'right';
        PageMethods.validate_FK_PAK_POLineRecDoc_ItemNo(value, this.validated_FK_PAK_POLineRecDoc_ItemNo);
      },
    validated_FK_PAK_POLineRecDoc_ItemNo_main: false,
    validated_FK_PAK_POLineRecDoc_ItemNo: function(result) {
      var p = result.split('|');
      var o = $get(p[1]);
      if(script_pakSTCPOLRD.validated_FK_PAK_POLineRecDoc_ItemNo_main){
        var o_d = $get(p[1]+'_Display');
        try{o_d.innerHTML = p[2];}catch(ex){}
      }
      o.style.backgroundImage  = 'none';
      if(p[0]=='1'){
        o.value='';
        o.focus();
      }
    },
    validate_FK_PAK_POLineRecDoc_UploadNo: function(o,Prefix) {
      var value = o.id;
      var SerialNo = $get(Prefix + 'SerialNo');
      if(SerialNo.value==''){
        if(this.validated_FK_PAK_POLineRecDoc_UploadNo_main){
          var o_d = $get(Prefix + 'SerialNo' + '_Display');
          try{o_d.innerHTML = '';}catch(ex){}
        }
        return true;
      }
      value = value + ',' + SerialNo.value ;
      var ItemNo = $get(Prefix + 'ItemNo');
      if(ItemNo.value==''){
        if(this.validated_FK_PAK_POLineRecDoc_UploadNo_main){
          var o_d = $get(Prefix + 'ItemNo' + '_Display');
          try{o_d.innerHTML = '';}catch(ex){}
        }
        return true;
      }
      value = value + ',' + ItemNo.value ;
      var UploadNo = $get(Prefix + 'UploadNo');
      if(UploadNo.value==''){
        if(this.validated_FK_PAK_POLineRecDoc_UploadNo_main){
          var o_d = $get(Prefix + 'UploadNo' + '_Display');
          try{o_d.innerHTML = '';}catch(ex){}
        }
        return true;
      }
      value = value + ',' + UploadNo.value ;
        o.style.backgroundImage  = 'url(../../images/pkloader.gif)';
        o.style.backgroundRepeat= 'no-repeat';
        o.style.backgroundPosition = 'right';
        PageMethods.validate_FK_PAK_POLineRecDoc_UploadNo(value, this.validated_FK_PAK_POLineRecDoc_UploadNo);
      },
    validated_FK_PAK_POLineRecDoc_UploadNo_main: false,
    validated_FK_PAK_POLineRecDoc_UploadNo: function(result) {
      var p = result.split('|');
      var o = $get(p[1]);
      if(script_pakSTCPOLRD.validated_FK_PAK_POLineRecDoc_UploadNo_main){
        var o_d = $get(p[1]+'_Display');
        try{o_d.innerHTML = p[2];}catch(ex){}
      }
      o.style.backgroundImage  = 'none';
      if(p[0]=='1'){
        o.value='';
        o.focus();
      }
    },
    temp: function() {
    }
    }
</script>
