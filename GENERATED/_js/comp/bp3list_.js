﻿Ext.require(["Ext.form.*"]);bp3list_=null;function bp3list_Panel_(d,e,f){var h=Ext.create("Ext.data.Store",{model:"model_bp3list_def",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3list_def/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"},extraParams:{instanceid:d}}});var g=Ext.create("Ext.data.Store",{model:"model_bp3list_col",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3list_col/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"},extraParams:{instanceid:d}}});var i=Ext.create("Ext.data.Store",{model:"model_bp3list_filter",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3list_filter/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"},extraParams:{instanceid:d}}});var j=Ext.create("Ext.data.Store",{model:"model_bp3list_filterfield",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3list_filterfield/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"}}});DefineForms_bp3list_def_();DefineForms_bp3list_col_();DefineForms_bp3list_filter_();var b=DefineInterface_bp3list_def_("int_bp3list_def",h,f);var a=DefineInterface_bp3list_col_("int_bp3list_col",g);var c=DefineInterface_bp3list_filter_("int_bp3list_filter",i);bp3list_=Ext.create("Ext.form.Panel",{id:"bp3list",layout:"fit",fieldDefaults:{labelAlign:"top",msgTarget:"side"},defaults:{anchor:"100%"},instanceid:d,onCommit:function(){},onButtonOk:function(){var k=this;b.doSave(k.onCommit)},onButtonCancel:function(){},canClose:function(){return b.canClose()},items:[{xtype:"tabpanel",itemId:"tabs_bp3list",activeTab:0,layout:"fit",tabPosition:"top",border:0,items:[{xtype:"panel",border:0,title:"Журнал",layout:"fit",itemId:"tab_bp3list_def",items:[b]},{xtype:"panel",border:0,title:"Колонки журнала",layout:"fit",itemId:"tab_bp3list_col",items:[a]},{xtype:"panel",border:0,title:"Группа полей фильтра",layout:"fit",itemId:"tab_bp3list_filter",items:[c]}]}]});if(e==true){bp3list_.closable=true;bp3list_.title="Журнал документов";bp3list_.frame=true}else{bp3list_.closable=false;bp3list_.title="";bp3list_.frame=false}h.on("load",function(){if(h.count()==0){h.insert(0,new model_bp3list_def())}record=h.getAt(0);record.instanceid=d;b.setActiveRecord(record,d)});h.load({params:{instanceid:d}});a.instanceid=d;g.load({params:{instanceid:d}});c.instanceid=d;i.load({params:{instanceid:d}});return bp3list_};