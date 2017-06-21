﻿Ext.require(["Ext.form.*"]);function DefineInterface_bp3card_fld_(c,d){var b=Ext.create("Ext.grid.feature.Grouping",{groupByText:"Группировать по этому полю",showGroupsText:"Показать группировку"});var a={menuFilterText:"Фильтр",ftype:"filters",local:true};var j;function g(k){if(k){Ext.Ajax.request({url:rootURL+"index.php/c_bp3card_fld/deleteRow",method:"POST",params:{bp3card_fldid:k.get("bp3card_fldid")}});j.store.remove(k)}}function f(){var k=j.getView().getSelectionModel().getSelection()[0];if(k){if(CheckOperation("bp3card.edit")!=0){Ext.Msg.show({title:"Удалить?",msg:"Удалить строку из базы данных?",buttons:Ext.Msg.YESNO,icon:Ext.MessageBox.QUESTION,fn:function(l,n,m){if(l=="yes"){g(m.selectedRow)}},caller:this,selectedRow:k})}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Удаление строк не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}}function e(){if(CheckOperation("bp3card.edit")!=0){var k=Ext.create("EditWindow_bp3card_fld");j.store.insert(0,new model_bp3card_fld());record=j.store.getAt(0);record.set("instanceid",j.instanceid);k.getComponent(0).setActiveRecord(record,j.instanceid);k.show()}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Создание строк не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}function i(){j.store.load({params:{instanceid:j.instanceid}})}function h(){var l=j.getView().getSelectionModel().getSelection()[0];if(l){if(CheckOperation("bp3card.edit")!=0){var k=Ext.create("EditWindow_bp3card_fld");k.getComponent(0).setActiveRecord(l,l.get("instanceid"));k.show()}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Изменение строк не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}}j=new Ext.grid.Panel({itemId:c,store:d,width:600,header:false,layout:"fit",scroll:"both",stateful:stateFulSystem,stateId:"bp3card_fld",iconCls:"icon-grid",frame:true,instanceid:"",features:[b,a],dockedItems:[{xtype:"toolbar",items:[{iconCls:"icon-application_form_add",text:"Создать",scope:this,handler:e},{iconCls:"icon-application_form_edit",text:"Изменить",scope:this,disabled:true,itemId:"edit",handler:h},{iconCls:"icon-application_form_delete",text:"Удалить",disabled:true,itemId:"delete",scope:this,handler:f},{iconCls:"icon-table_refresh",text:"Обновить",itemId:"bRefresh",scope:this,handler:i}]}],columns:[{text:"№ п/п",width:60,dataIndex:"sequence",sortable:true},{text:"Обязательное поле",width:80,dataIndex:"mandatoryfield_grid",sortable:true},{text:"Поле, на которое накладывается ограничение",width:200,dataIndex:"thefield_grid",sortable:true},{text:"Структура, которой принадлежит поле",width:200,dataIndex:"thepart_grid",sortable:true},{text:"Разрешена модификация",width:80,dataIndex:"allowmodify_grid",sortable:true},{text:"Разрешен просмотр",width:80,dataIndex:"allowread_grid",sortable:true},{text:"Имя вкладки",width:200,dataIndex:"tabname",sortable:true},{text:"Имя группы",width:200,dataIndex:"fieldgroupbox",sortable:true},{text:"Стиль",width:200,dataIndex:"thestyle",sortable:true},{text:"Надпись",width:200,dataIndex:"caption",sortable:true}],listeners:{render:function(k){k.store.on("load",function(n,m,l){if(n.count()>0){k.getSelectionModel().select(0)}})},itemdblclick:function(){h()},itemclick:function(l,k){j.down("#delete").setDisabled(false);j.down("#edit").setDisabled(false)},select:function(n,m,l,k){j.down("#delete").setDisabled(false);j.down("#edit").setDisabled(false)},selectionchange:function(l,k){j.down("#delete").setDisabled(k.length===0);j.down("#edit").setDisabled(k.length===0)}}});return j}function DefineForms_bp3card_fld_(){Ext.define("Form_bp3card_fld",{extend:"Ext.form.Panel",alias:"widget.f_bp3card_fld",initComponent:function(){this.addEvents("create");Ext.apply(this,{activeRecord:null,defaultType:"textfield",x:0,fieldDefaults:{labelAlign:"top"},items:[{xtype:"panel",closable:false,title:"",preventHeader:true,id:"bp3card_fld-0",layout:"absolute",border:false,items:[{minWidth:370,width:370,maxWidth:370,x:5,y:0,xtype:"numberfield",value:"0",name:"sequence",itemId:"sequence",fieldLabel:"№ п/п",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:380,y:0,xtype:"combobox",editable:false,trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue()},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{this.expand()}},store:enum_TriState,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"mandatoryfield_grid",itemId:"mandatoryfield_grid",listeners:{select:function(a,c,b){a.up("form").activeRecord.set("mandatoryfield",c[0].get("value"))}},fieldLabel:"Обязательное поле",allowBlank:true,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:5,y:46,xtype:"combobox",trigger1Cls:"x-form-select-trigger",hideTrigger1:false,onTrigger1Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(a,c,b){a.up("form").activeRecord.set("thefield",c[0].get("id"))}},store:cmbstore_bp3card_fld,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"thefield_grid",itemId:"thefield_grid",fieldLabel:"Поле, на которое накладывается ограничение",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:380,y:46,xtype:"combobox",trigger1Cls:"x-form-select-trigger",hideTrigger1:false,onTrigger1Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:false,listeners:{select:function(a,c,b){a.up("form").activeRecord.set("thepart",c[0].get("id"))}},store:cmbstore_bp3card_part,valueField:"brief",displayField:"brief",typeAhead:true,emptyText:"",name:"thepart_grid",itemId:"thepart_grid",fieldLabel:"Структура, которой принадлежит поле",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:5,y:92,xtype:"combobox",editable:false,store:enum_Boolean,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"allowmodify_grid",itemId:"allowmodify_grid",listeners:{select:function(a,c,b){a.up("form").activeRecord.set("allowmodify",c[0].get("value"))}},fieldLabel:"Разрешена модификация",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:380,y:92,xtype:"combobox",editable:false,store:enum_Boolean,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"allowread_grid",itemId:"allowread_grid",listeners:{select:function(a,c,b){a.up("form").activeRecord.set("allowread",c[0].get("value"))}},fieldLabel:"Разрешен просмотр",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:5,y:138,xtype:"textfield",value:"",name:"tabname",itemId:"tabname",fieldLabel:"Имя вкладки",allowBlank:true,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:380,y:138,xtype:"textfield",value:"",name:"fieldgroupbox",itemId:"fieldgroupbox",fieldLabel:"Имя группы",allowBlank:true,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:5,y:184,xtype:"textfield",value:"",name:"thestyle",itemId:"thestyle",fieldLabel:"Стиль",allowBlank:true,labelWidth:140},{minWidth:370,width:370,maxWidth:370,x:380,y:184,xtype:"textfield",value:"",name:"caption",itemId:"caption",fieldLabel:"Надпись",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:140}],width:770,height:260}],instanceid:"",dockedItems:[{xtype:"toolbar",dock:"bottom",ui:"footer",items:["->",{iconCls:"icon-accept",itemId:"save",text:"Сохранить",disabled:true,scope:this,handler:this.onSave},{iconCls:"icon-cancel",text:"Закрыть",scope:this,handler:this.onReset}]}]});this.callParent()},setActiveRecord:function(b,a){this.activeRecord=b;this.instanceid=a;if(b){this.down("#save").enable();this.getForm().loadRecord(b)}else{this.down("#save").disable();this.getForm().reset()}},onSave:function(){var a=this.activeRecord,b=this.getForm();if(!a){return}if(b.isValid()){b.updateRecord(a);StatusDB("Сохранение данных");Ext.Ajax.request({url:rootURL+"index.php/c_bp3card_fld/setRow",method:"POST",params:{instanceid:this.instanceid,bp3card_fldid:a.get("bp3card_fldid"),sequence:a.get("sequence"),mandatoryfield:a.get("mandatoryfield"),thefield:a.get("thefield"),thepart:a.get("thepart"),allowmodify:a.get("allowmodify"),allowread:a.get("allowread"),tabname:a.get("tabname"),fieldgroupbox:a.get("fieldgroupbox"),thestyle:a.get("thestyle"),caption:a.get("caption")},success:function(d){var e=d.responseText;var c=Ext.decode(e);if(c.success==false){Ext.MessageBox.show({title:"Ошибка",msg:c.msg,buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR});StatusErr("Ошибка. "+c.msg)}else{if(a.get("bp3card_fldid")==""){a.set("bp3card_fldid",c.data.bp3card_fldid)}StatusReady("Изменения сохранены");b.owner.ownerCt.close()}}})}else{Ext.MessageBox.show({title:"Ошибка",msg:"Не все обязательные поля заполнены!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR})}},onReset:function(){if(this.activeRecord.get("bp3card_fldid")==""){this.activeRecord.store.reload()}this.setActiveRecord(null,null);this.ownerCt.close()}});Ext.define("EditWindow_bp3card_fld",{extend:"Ext.window.Window",maxHeight:365,maxWidth:900,autoScroll:true,minWidth:750,width:800,minHeight:335,height:335,constrainHeader:true,layout:"absolute",autoShow:true,modal:true,closable:false,closeAction:"destroy",iconCls:"icon-application_form",title:"Поля карточки",items:[{xtype:"f_bp3card_fld"}]})};