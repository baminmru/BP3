﻿Ext.require(["Ext.form.*"]);function DefineInterface_bp3list_col_(c,d){var b=Ext.create("Ext.grid.feature.Grouping",{groupByText:"Группировать по этому полю",showGroupsText:"Показать группировку"});var a={menuFilterText:"Фильтр",ftype:"filters",local:true};var j;function g(k){if(k){Ext.Ajax.request({url:rootURL+"index.php/c_bp3list_col/deleteRow",method:"POST",params:{bp3list_colid:k.get("bp3list_colid")}});j.store.remove(k)}}function f(){var k=j.getView().getSelectionModel().getSelection()[0];if(k){if(CheckOperation("bp3list.edit")!=0){Ext.Msg.show({title:"Удалить?",msg:"Удалить строку из базы данных?",buttons:Ext.Msg.YESNO,icon:Ext.MessageBox.QUESTION,fn:function(l,n,m){if(l=="yes"){g(m.selectedRow)}},caller:this,selectedRow:k})}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Удаление строк не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}}function e(){if(CheckOperation("bp3list.edit")!=0){var k=Ext.create("EditWindow_bp3list_col");j.store.insert(0,new model_bp3list_col());record=j.store.getAt(0);record.set("instanceid",j.instanceid);k.getComponent(0).setActiveRecord(record,j.instanceid);k.show()}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Создание строк не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}function i(){j.store.load({params:{instanceid:j.instanceid}})}function h(){var l=j.getView().getSelectionModel().getSelection()[0];if(l){if(CheckOperation("bp3list.edit")!=0){var k=Ext.create("EditWindow_bp3list_col");k.getComponent(0).setActiveRecord(l,l.get("instanceid"));k.show()}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Изменение строк не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}}j=new Ext.grid.Panel({itemId:c,store:d,width:600,header:false,layout:"fit",scroll:"both",stateful:stateFulSystem,stateId:"bp3list_col",iconCls:"icon-grid",frame:true,instanceid:"",features:[b,a],dockedItems:[{xtype:"toolbar",items:[{iconCls:"icon-application_form_add",text:"Создать",scope:this,handler:e},{iconCls:"icon-application_form_edit",text:"Изменить",scope:this,disabled:true,itemId:"edit",handler:h},{iconCls:"icon-application_form_delete",text:"Удалить",disabled:true,itemId:"delete",scope:this,handler:f},{iconCls:"icon-table_refresh",text:"Обновить",itemId:"bRefresh",scope:this,handler:i}]}],columns:[{text:"Последовательность",width:60,dataIndex:"sequence",sortable:true},{text:"Название",width:200,dataIndex:"name",sortable:true},{text:"Поле представления",width:200,dataIndex:"viewfield",sortable:true},{text:"Ширина колонки",width:60,dataIndex:"colwidth",sortable:true},{text:"Выравнивание",width:80,dataIndex:"columnalignment_grid",sortable:true},{text:"Сортировка колонки",width:80,dataIndex:"colsort_grid",sortable:true},{text:"Стиль",width:200,dataIndex:"thestyle",sortable:true},{text:"Аггрегация при группировке",width:80,dataIndex:"groupaggregation_grid",sortable:true}],listeners:{render:function(k){k.store.on("load",function(n,m,l){if(n.count()>0){k.getSelectionModel().select(0)}})},itemdblclick:function(){h()},itemclick:function(l,k){j.down("#delete").setDisabled(false);j.down("#edit").setDisabled(false)},select:function(n,m,l,k){j.down("#delete").setDisabled(false);j.down("#edit").setDisabled(false)},selectionchange:function(l,k){j.down("#delete").setDisabled(k.length===0);j.down("#edit").setDisabled(k.length===0)}}});return j}function DefineForms_bp3list_col_(){Ext.define("Form_bp3list_col",{extend:"Ext.form.Panel",alias:"widget.f_bp3list_col",initComponent:function(){this.addEvents("create");Ext.apply(this,{activeRecord:null,defaultType:"textfield",x:0,fieldDefaults:{labelAlign:"top"},items:[{xtype:"panel",closable:false,title:"",preventHeader:true,id:"bp3list_col-0",layout:"absolute",border:false,items:[{minWidth:740,width:740,maxWidth:740,x:5,y:0,xtype:"numberfield",value:"0",name:"sequence",itemId:"sequence",fieldLabel:"Последовательность",allowBlank:true,labelWidth:120},{minWidth:740,width:740,maxWidth:740,x:5,y:46,xtype:"textfield",value:"",name:"name",itemId:"name",fieldLabel:"Название",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:120},{minWidth:740,width:740,maxWidth:740,x:5,y:92,xtype:"textfield",value:"",name:"viewfield",itemId:"viewfield",fieldLabel:"Поле представления",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:120},{minWidth:740,width:740,maxWidth:740,x:5,y:138,xtype:"numberfield",value:"0",name:"colwidth",itemId:"colwidth",fieldLabel:"Ширина колонки",allowBlank:true,labelWidth:120},{minWidth:740,width:740,maxWidth:740,x:5,y:184,xtype:"combobox",editable:false,store:enum_VHAlignment,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"columnalignment_grid",itemId:"columnalignment_grid",listeners:{select:function(a,c,b){a.up("form").activeRecord.set("columnalignment",c[0].get("value"))}},fieldLabel:"Выравнивание",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:120},{minWidth:740,width:740,maxWidth:740,x:5,y:230,xtype:"combobox",editable:false,store:enum_ColumnSortType,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"colsort_grid",itemId:"colsort_grid",listeners:{select:function(a,c,b){a.up("form").activeRecord.set("colsort",c[0].get("value"))}},fieldLabel:"Сортировка колонки",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:120},{minWidth:740,width:740,maxWidth:740,x:5,y:276,xtype:"textfield",value:"",name:"thestyle",itemId:"thestyle",fieldLabel:"Стиль",allowBlank:true,labelWidth:120},{minWidth:740,width:740,maxWidth:740,x:5,y:322,xtype:"combobox",editable:false,store:enum_AggregationType,valueField:"name",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"groupaggregation_grid",itemId:"groupaggregation_grid",listeners:{select:function(a,c,b){a.up("form").activeRecord.set("groupaggregation",c[0].get("value"))}},fieldLabel:"Аггрегация при группировке",labelClsExtra:"x-item-mandatory",allowBlank:false,labelWidth:120}],width:770,height:398}],instanceid:"",dockedItems:[{xtype:"toolbar",dock:"bottom",ui:"footer",items:["->",{iconCls:"icon-accept",itemId:"save",text:"Сохранить",disabled:true,scope:this,handler:this.onSave},{iconCls:"icon-cancel",text:"Закрыть",scope:this,handler:this.onReset}]}]});this.callParent()},setActiveRecord:function(b,a){this.activeRecord=b;this.instanceid=a;if(b){this.down("#save").enable();this.getForm().loadRecord(b)}else{this.down("#save").disable();this.getForm().reset()}},onSave:function(){var a=this.activeRecord,b=this.getForm();if(!a){return}if(b.isValid()){b.updateRecord(a);StatusDB("Сохранение данных");Ext.Ajax.request({url:rootURL+"index.php/c_bp3list_col/setRow",method:"POST",params:{instanceid:this.instanceid,bp3list_colid:a.get("bp3list_colid"),sequence:a.get("sequence"),name:a.get("name"),viewfield:a.get("viewfield"),colwidth:a.get("colwidth"),columnalignment:a.get("columnalignment"),colsort:a.get("colsort"),thestyle:a.get("thestyle"),groupaggregation:a.get("groupaggregation")},success:function(d){var e=d.responseText;var c=Ext.decode(e);if(c.success==false){Ext.MessageBox.show({title:"Ошибка",msg:c.msg,buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR});StatusErr("Ошибка. "+c.msg)}else{if(a.get("bp3list_colid")==""){a.set("bp3list_colid",c.data.bp3list_colid)}StatusReady("Изменения сохранены");b.owner.ownerCt.close()}}})}else{Ext.MessageBox.show({title:"Ошибка",msg:"Не все обязательные поля заполнены!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.ERROR})}},onReset:function(){if(this.activeRecord.get("bp3list_colid")==""){this.activeRecord.store.reload()}this.setActiveRecord(null,null);this.ownerCt.close()}});Ext.define("EditWindow_bp3list_col",{extend:"Ext.window.Window",maxHeight:503,maxWidth:900,autoScroll:true,minWidth:750,width:800,minHeight:473,height:473,constrainHeader:true,layout:"absolute",autoShow:true,modal:true,closable:false,closeAction:"destroy",iconCls:"icon-application_form",title:"Колонки журнала",items:[{xtype:"f_bp3list_col"}]})};