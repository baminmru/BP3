﻿Ext.require(["Ext.form.*"]);bp3report_=null;function bp3report_Panel_(c,d,e){var f=Ext.create("Ext.data.Store",{model:"model_bp3report_def",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3report_def/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"},extraParams:{instanceid:c}}});var g=Ext.create("Ext.data.Store",{model:"model_bp3report_filter",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3report_filter/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"},extraParams:{instanceid:c}}});var h=Ext.create("Ext.data.Store",{model:"model_bp3report_filterfiel",autoLoad:false,autoSync:false,proxy:{type:"ajax",url:rootURL+"index.php/c_bp3report_filterfiel/getRows",reader:{type:"json",root:"data",successProperty:"success",messageProperty:"msg"}}});DefineForms_bp3report_def_();DefineForms_bp3report_filter_();var a=DefineInterface_bp3report_def_("int_bp3report_def",f,e);var b=DefineInterface_bp3report_filter_("int_bp3report_filter",g);bp3report_=Ext.create("Ext.form.Panel",{id:"bp3report",layout:"fit",fieldDefaults:{labelAlign:"top",msgTarget:"side"},defaults:{anchor:"100%"},instanceid:c,onCommit:function(){},onButtonOk:function(){var i=this;a.doSave(i.onCommit)},onButtonCancel:function(){},canClose:function(){return a.canClose()},items:[{xtype:"tabpanel",itemId:"tabs_bp3report",activeTab:0,layout:"fit",tabPosition:"top",border:0,items:[{xtype:"panel",border:0,title:"Описание",layout:"fit",itemId:"tab_bp3report_def",items:[a]},{xtype:"panel",border:0,title:"Группа полей фильтра",layout:"fit",itemId:"tab_bp3report_filter",items:[b]}]}]});if(d==true){bp3report_.closable=true;bp3report_.title="Отчет";bp3report_.frame=true}else{bp3report_.closable=false;bp3report_.title="";bp3report_.frame=false}f.on("load",function(){if(f.count()==0){f.insert(0,new model_bp3report_def())}record=f.getAt(0);record.instanceid=c;a.setActiveRecord(record,c)});f.load({params:{instanceid:c}});b.instanceid=c;g.load({params:{instanceid:c}});return bp3report_};