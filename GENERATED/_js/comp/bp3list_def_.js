﻿Ext.require(["Ext.form.*"]);function DefineInterface_bp3list_def_(a,b,j){var g;var h=false;var i=false;function d(m,l){var k=g.activeRecord,n=g.getForm();if(!k){return}if(n.isValid()){n.updateRecord(k);StatusDB("Сохранение данных");Ext.Ajax.request({url:rootURL+"index.php/c_bp3list_def/setRow",method:"POST",params:{instanceid:g.instanceid,bp3list_defid:k.get("bp3list_defid"),name:k.get("name"),thecomment:k.get("thecomment"),sourceview:k.get("sourceview"),onrun:k.get("onrun"),editcard:k.get("editcard"),newcard:k.get("newcard")},success:function(p){var q=p.responseText;var o=Ext.decode(q);if(o.success==false){Ext.MessageBox.show({title:"Ошибка",msg:o.msg,buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR});StatusErr("Ошибка. "+o.msg);h=false}else{if(k.get("bp3list_defid")==""){k.set("bp3list_defid",o.data.bp3list_defid)}StatusReady("Изменения сохранены");h=true;if(j){Ext.Ajax.request({url:rootURL+'index.php/c_v_autobp3list_def/getRows?&filter=[{"property":"bp3list_defid","value":"'+k.get("bp3list_defid")+'"}]',method:"GET",success:function(s){var r=Ext.decode(s.responseText);j.set(r.rows[0]);j.commit()}})}if(m){if(typeof(l)=="function"){l()}g.up("window").close()}}}})}else{Ext.MessageBox.show({title:"Ошибка",msg:"Не все обязательные поля заполнены!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR})}}function e(k){d(false,k)}function f(k){d(true,k)}function c(){g.setActiveRecord(null,null)}g=new Ext.form.Panel({itemId:a+"",autoScroll:true,border:0,bodyPadding:5,activeRecord:null,selection:j,defaultType:"textfield",doSave:f,canClose:function(){if(i){if(!g.getForm().isValid()){return true}return true}else{if(!g.getForm().isValid()){return false}if(h){return true}return false}},fieldDefaults:{labelAlign:"right",labelWidth:110},items:[{xtype:"fieldset",anchor:"100%",id:"bp3list_def-0",x:0,border:1,layout:"absolute",items:[{minWidth:365,width:365,maxWidth:365,x:5,y:5,labelWidth:140,xtype:"textfield",value:"",name:"name",itemId:"name",fieldLabel:"Название",labelClsExtra:"x-item-mandatory",allowBlank:false},{minWidth:720,xtype:"textarea",x:5,y:35,height:80,labelWidth:140,xtype:"textarea",value:"",name:"thecomment",itemId:"thecomment",fieldLabel:"Описание",allowBlank:true},{minWidth:365,width:365,maxWidth:365,x:5,y:125,labelWidth:140,xtype:"combobox",trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue();this.up("form").activeRecord.set("sourceview",null)},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(k,m,l){k.up("form").activeRecord.set("sourceview",m[0].get("id"))}},store:cmbstore_bp3qry_def,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"sourceview_grid",itemId:"sourceview_grid",fieldLabel:"Запрос",allowBlank:true},{minWidth:365,width:365,maxWidth:365,x:375,y:125,labelWidth:140,xtype:"combobox",editable:false,store:enum_OnJournalRowClick,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"onrun_grid",itemId:"onrun_grid",listeners:{select:function(k,m,l){k.up("form").activeRecord.set("onrun",m[0].get("value"))}},fieldLabel:"При открытии",labelClsExtra:"x-item-mandatory",allowBlank:false},{minWidth:365,width:365,maxWidth:365,x:5,y:155,labelWidth:140,xtype:"combobox",trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue();this.up("form").activeRecord.set("editcard",null)},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(k,m,l){k.up("form").activeRecord.set("editcard",m[0].get("id"))}},store:cmbstore_bp3card_def,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"editcard_grid",itemId:"editcard_grid",fieldLabel:"Карточка для редактирования",allowBlank:true},{minWidth:365,width:365,maxWidth:365,x:375,y:155,labelWidth:140,xtype:"combobox",trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue();this.up("form").activeRecord.set("newcard",null)},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(k,m,l){k.up("form").activeRecord.set("newcard",m[0].get("id"))}},store:cmbstore_bp3card_def,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"newcard_grid",itemId:"newcard_grid",fieldLabel:"Карточка для создания",allowBlank:true}],height:205}],instanceid:"",setActiveRecord:function(l,k){g.activeRecord=l;g.instanceid=k;if(l){g.getForm().loadRecord(l);i=g.getForm().isValid()}else{g.getForm().reset()}}});return g}function DefineForms_bp3list_def_(){Ext.define("Form_bp3list_def",{extend:"Ext.form.Panel",alias:"widget.f_bp3list_def",initComponent:function(){this.addEvents("create");Ext.apply(this,{activeRecord:null,defaultType:"textfield",x:0,fieldDefaults:{labelAlign:"top"},items:[{xtype:"panel",closable:false,title:"",preventHeader:true,id:"bp3list_def-0",layout:"absolute",border:false,items:[{minWidth:220,width:220,maxWidth:220,x:5,y:0,xtype:"textfield",value:"",name:"name",itemId:"name",fieldLabel:"Название",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:120},{minWidth:740,width:740,xtype:"textarea",x:5,y:46,height:80,xtype:"textarea",value:"",name:"thecomment",itemId:"thecomment",fieldLabel:"Описание",allowBlank:true,labelWidth:120},{minWidth:220,width:220,maxWidth:220,x:5,y:136,xtype:"combobox",trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue();this.up("form").activeRecord.set("sourceview",null)},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(a,c,b){a.up("form").activeRecord.set("sourceview",c[0].get("id"))}},store:cmbstore_bp3qry_def,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"sourceview_grid",itemId:"sourceview_grid",fieldLabel:"Запрос",allowBlank:true,labelWidth:120},{minWidth:220,width:220,maxWidth:220,x:255,y:136,xtype:"combobox",editable:false,store:enum_OnJournalRowClick,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"onrun_grid",itemId:"onrun_grid",listeners:{select:function(a,c,b){a.up("form").activeRecord.set("onrun",c[0].get("value"))}},fieldLabel:"При открытии",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:120},{minWidth:220,width:220,maxWidth:220,x:505,y:136,xtype:"combobox",trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue();this.up("form").activeRecord.set("editcard",null)},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(a,c,b){a.up("form").activeRecord.set("editcard",c[0].get("id"))}},store:cmbstore_bp3card_def,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"editcard_grid",itemId:"editcard_grid",fieldLabel:"Карточка для редактирования",allowBlank:true,labelWidth:120},{minWidth:220,width:220,maxWidth:220,x:5,y:182,xtype:"combobox",trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue();this.up("form").activeRecord.set("newcard",null)},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(a,c,b){a.up("form").activeRecord.set("newcard",c[0].get("id"))}},store:cmbstore_bp3card_def,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"newcard_grid",itemId:"newcard_grid",fieldLabel:"Карточка для создания",allowBlank:true,labelWidth:120}],width:770,height:258}],instanceid:"",dockedItems:[{xtype:"toolbar",dock:"bottom",ui:"footer",items:["->",{iconCls:"icon-accept",itemId:"save",text:"Сохранить",disabled:true,scope:this,handler:this.onSave}]}]});this.callParent()},setActiveRecord:function(b,a){this.activeRecord=b;this.instanceid=a;if(b){this.down("#save").enable();this.getForm().loadRecord(b)}else{this.down("#save").disable();this.getForm().reset()}},onSave:function(){var a=this.activeRecord,b=this.getForm();if(!a){return}if(b.isValid()){b.updateRecord(a);StatusDB("Сохранение данных");Ext.Ajax.request({url:rootURL+"index.php/c_bp3list_def/setRow",method:"POST",params:{instanceid:this.instanceid,bp3list_defid:a.get("bp3list_defid"),name:a.get("name"),thecomment:a.get("thecomment"),sourceview:a.get("sourceview"),onrun:a.get("onrun"),editcard:a.get("editcard"),newcard:a.get("newcard")},success:function(d){var e=d.responseText;var c=Ext.decode(e);if(c.success==false){Ext.MessageBox.show({title:"Ошибка",msg:c.msg,buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR});StatusErr("Ошибка. "+c.msg)}else{if(a.get("bp3list_defid")==""){a.set("bp3list_defid",c.data.bp3list_defid)}StatusReady("Изменения сохранены")}}})}else{Ext.MessageBox.show({title:"Ошибка",msg:"Не все обязательные поля заполнены!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR})}},onReset:function(){if(this.activeRecord.get("bp3list_defid")==""){this.activeRecord.store.reload()}this.setActiveRecord(null,null);this.ownerCt.close()}});Ext.define("EditWindow_bp3list_def",{extend:"Ext.window.Window",maxHeight:363,maxWidth:900,autoScroll:true,minWidth:750,width:800,minHeight:333,height:333,constrainHeader:true,layout:"absolute",autoShow:true,modal:true,closable:false,closeAction:"destroy",iconCls:"icon-application_form",title:"Журнал",items:[{xtype:"f_bp3list_def"}]})};