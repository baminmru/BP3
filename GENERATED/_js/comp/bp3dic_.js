﻿Ext.require(["Ext.form.*"]);bp3dic_=null;function bp3dic_Panel_(b,c,d){var e=Ext.create("Ext.data.Store",{model:"model_bp3dic_gen",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3dic_gen/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"},extraParams:{instanceid:b}}});DefineForms_bp3dic_gen_();var a=DefineInterface_bp3dic_gen_("int_bp3dic_gen",e);bp3dic_=Ext.create("Ext.form.Panel",{id:"bp3dic",layout:"fit",fieldDefaults:{labelAlign:"top",msgTarget:"side"},defaults:{anchor:"100%"},instanceid:b,onCommit:function(){},onButtonOk:function(){var f=this},onButtonCancel:function(){},canClose:function(){return true},items:[{xtype:"tabpanel",itemId:"tabs_bp3dic",activeTab:0,layout:"fit",tabPosition:"top",border:0,items:[{xtype:"panel",border:0,title:"Генераторы",layout:"fit",itemId:"tab_bp3dic_gen",items:[a]}]}]});if(c==true){bp3dic_.closable=true;bp3dic_.title="Справочники моделлера";bp3dic_.frame=true}else{bp3dic_.closable=false;bp3dic_.title="";bp3dic_.frame=false}a.instanceid=b;e.load({params:{instanceid:b}});return bp3dic_};