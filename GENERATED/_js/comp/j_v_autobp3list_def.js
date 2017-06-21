﻿var groupingFeature_autobp3list_def=Ext.create("Ext.grid.feature.Grouping",{groupByText:"Группировать по этому полю",showGroupsText:"Показать группировку"});var interval_autobp3list_def;Ext.define("grid_autobp3list_def",{extend:"Ext.grid.Panel",alias:"widget.g_v_autobp3list_def",requires:["Ext.grid.*","Ext.form.field.Text","Ext.toolbar.TextItem"],initComponent:function(){Ext.apply(this,{frame:false,store:store_v_autobp3list_def,features:[groupingFeature_autobp3list_def],defaultDockWeights:{top:7,bottom:5,left:1,right:3},viewConfig:{enableTextSelection:true},dockedItems:[{xtype:"toolbar",items:[{iconCls:"icon-application_form_add",text:"Создать",scope:this,handler:this.onAddClick},{iconCls:"icon-application_form_edit",text:"Изменить",itemId:"edit",disabled:true,scope:this,handler:this.onEditClick},{iconCls:"icon-application_form_delete",text:"Удалить",disabled:true,itemId:"delete",scope:this,handler:this.onDeleteClick},{iconCls:"icon-table_refresh",text:"Обновить",itemId:"bRefresh",scope:this,handler:this.onRefreshClick},{iconCls:"icon-page_excel",text:"Экспорт",itemId:"bExport",scope:this,handler:this.onExportClick}]}],columns:[{text:"Название",width:133,dataIndex:"bp3list_def_name",sortable:true},{text:"Описание",width:133,dataIndex:"bp3list_def_thecomment",sortable:true},{text:"Запрос",width:133,dataIndex:"bp3list_def_sourceview",sortable:true},{text:"При открытии",width:133,dataIndex:"bp3list_def_onrun",sortable:true},{text:"Карточка для редактирования",width:133,dataIndex:"bp3list_def_editcard",sortable:true},{text:"Карточка для создания",width:133,dataIndex:"bp3list_def_newcard",sortable:true}],bbar:Ext.create("Ext.PagingToolbar",{store:store_v_autobp3list_def,displayInfo:true,displayMsg:"Показаны строки с {0} по {1} из {2}",emptyMsg:"нет данных"}),rbar:[{xtype:"form",title:"Фильтры",iconCls:"icon-find",flex:1,collapsible:true,collapseDirection:"right",animCollapse:false,titleCollapse:true,bodyPadding:5,width:200,minWidth:200,autoScroll:true,buttonAlign:"center",layout:{type:"vbox",align:"stretch"},defaultType:"textfield",items:[{value:"",name:"bp3list_def_name",itemId:"bp3list_def_name",fieldLabel:"",emptyText:"Название",hideLabel:true,listeners:{render:function(a){Ext.QuickTips.register({target:a.getEl(),text:"Название"})}}},{xtype:"textfield",value:"",name:"bp3list_def_thecomment",itemId:"bp3list_def_thecomment",fieldLabel:"",emptyText:"Описание",hideLabel:true,listeners:{render:function(a){Ext.QuickTips.register({target:a.getEl(),text:"Описание"})}}},{trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue()},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:true,enableRegEx:true,queryMode:"local",listeners:{focus:function(){if(this.store.count(false)==0){this.store.load()}},render:function(a){Ext.QuickTips.register({target:a.getEl(),text:"Запрос"})}},xtype:"combobox",store:cmbstore_bp3qry_def,valueField:"id",displayField:"brief",typeAhead:true,name:"bp3list_def_sourceview_id",itemId:"bp3list_def_sourceview_id",fieldLabel:"",emptyText:"Запрос",hideLabel:true},{xtype:"combobox",editable:false,trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue()},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{this.expand()}},store:enum_OnJournalRowClick,valueField:"value",displayField:"name",typeAhead:true,queryMode:"local",emptyText:"",name:"bp3list_def_onrun_val",itemId:"bp3list_def_onrun_val",fieldLabel:"",emptyText:"При открытии",hideLabel:true,listeners:{render:function(a){Ext.QuickTips.register({target:a.getEl(),text:"При открытии"})}}},{trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue()},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:true,enableRegEx:true,queryMode:"local",listeners:{focus:function(){if(this.store.count(false)==0){this.store.load()}},render:function(a){Ext.QuickTips.register({target:a.getEl(),text:"Карточка для редактирования"})}},xtype:"combobox",store:cmbstore_bp3card_def,valueField:"id",displayField:"brief",typeAhead:true,name:"bp3list_def_editcard_id",itemId:"bp3list_def_editcard_id",fieldLabel:"",emptyText:"Карточка для редактирования",hideLabel:true},{trigger1Cls:"x-form-clear-trigger",trigger2Cls:"x-form-select-trigger",hideTrigger1:false,hideTrigger2:false,onTrigger1Click:function(){this.collapse();this.clearValue()},onTrigger2Click:function(){if(this.isExpanded){this.collapse()}else{if(this.store.count(false)==0){this.store.load()}this.expand()}},editable:true,enableRegEx:true,queryMode:"local",listeners:{focus:function(){if(this.store.count(false)==0){this.store.load()}},render:function(a){Ext.QuickTips.register({target:a.getEl(),text:"Карточка для создания"})}},xtype:"combobox",store:cmbstore_bp3card_def,valueField:"id",displayField:"brief",typeAhead:true,name:"bp3list_def_newcard_id",itemId:"bp3list_def_newcard_id",fieldLabel:"",emptyText:"Карточка для создания",hideLabel:true}],buttons:[{text:"Найти",iconCls:"icon-find",formBind:true,disabled:false,grid:this,handler:function(){var e=this.up("form").getForm().getValues(false,true);var a=new Array();if(this.grid.default_filter!=null){for(var b=0;b<this.grid.default_filter.length;b++){var d=this.grid.default_filter[b];a.push({property:d.key,value:d.value})}}for(var c in e){a.push({property:c,value:e[c]})}if(this.grid.store.filters.length>0){this.grid.store.filters.clear()}if(a.length>0){this.grid.store.filter(a)}else{this.grid.store.load()}}},{text:"Сбросить",iconCls:"icon-cancel",grid:this,handler:function(){this.up("form").getForm().reset();var a=new Array();if(this.grid.default_filter!=null){for(var b=0;b<this.grid.default_filter.length;b++){var c=this.grid.default_filter[b];a.push({property:c.key,value:c.value})}}if(this.grid.store.filters.length>0){this.grid.store.filters.clear()}if(a.length>0){this.grid.store.filter(a)}else{this.grid.store.load()}}}]}]});this.callParent();this.getSelectionModel().on("selectionchange",this.onSelectChange,this);this.store.load()},onSelectChange:function(b,a){this.down("#delete").setDisabled(a.length===0);this.down("#edit").setDisabled(a.length===0)},listeners:{itemdblclick:function(){this.onEditClick()},added:function(){},destroy:function(){}},onDeleteConfirm:function(a){if(a){Ext.Ajax.request({url:rootURL+"index.php/c_v_autobp3list_def/deleteRow",method:"POST",params:{instanceid:a.get("instanceid")}});this.store.remove(a)}},onDeleteClick:function(){var a=this.getView().getSelectionModel().getSelection()[0];if(a){if(CheckOperation("bp3list.edit")!=0&&OTAllowDelete("bp3list")){Ext.Msg.show({title:"Удалить?",msg:"Удалить строку из базы данных?",buttons:Ext.Msg.YESNO,icon:Ext.MessageBox.QUESTION,fn:function(b,d,c){if(b=="yes"){c.caller.onDeleteConfirm(c.selectedRow)}},caller:this,selectedRow:a})}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Удаление объектов не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}},onAddClick:function(){if(CheckOperation("bp3list.edit")!=0&&OTAllowAdd("bp3list")){Ext.Ajax.request({url:rootURL+"index.php/c_v_autobp3list_def/newRow",method:"POST",params:{},success:function(response){var text=response.responseText;var res=Ext.decode(text);var edit=Ext.create("iu.windowObjects");edit.prefix="c_v_autobp3list_def";edit.setTitle("Создание документа:Журнал документов");var p=eval("bp3list_Panel_"+OTAddMode("bp3list")+"( res.data, false,null )");edit.add(p);edit.show()}});this.store.load()}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Создание объектов не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}},onEditClick:function(){var selection=this.getView().getSelectionModel().getSelection()[0];if(selection){if(CheckOperation("bp3list.edit")!=0){var edit=Ext.create("iu.windowObjects");edit.prefix="c_v_autobp3list_def";edit.setTitle("Редактирование документа: Журнал документов");var p=eval("bp3list_Panel_"+OTEditMode("bp3list")+"( selection.get('instanceid'), false, selection )");edit.add(p);edit.show()}else{Ext.MessageBox.show({title:"Контроль прав.",msg:"Изменение объектов не разрешено!",buttons:Ext.MessageBox.OK,icon:Ext.MessageBox.WARNING})}}},onRefreshClick:function(){this.store.load()},onExportClick:function(){var a={title:this.title,columns:this.columns};var b=new Workbook(a);b.addWorksheet(this.store,a);var c=b.render();window.open("data:application/vnd.ms-excel;base64,"+Base64.encode(c),"_blank")}});Ext.require(["Ext.form.*"]);function bp3list_Jrnl(){var a=Ext.create("Ext.form.Panel",{closable:true,id:"bp3list_jrnl",title:"Журнал документов",layout:"fit",flex:1,fieldDefaults:{labelAlign:"top",msgTarget:"side"},defaults:{anchor:"100%"},items:[{itemId:"gr_autobp3list_def",xtype:"g_v_autobp3list_def",stateful:stateFulSystem,stateId:"j_v_autobp3list_def",layout:"fit",flex:1,store:store_v_autobp3list_def}]});return a}Ext.define("ObjectWindow_bp3list",{extend:"Ext.window.Window",maxHeight:620,minHeight:620,minWidth:800,maxWidth:1024,constrainHeader:true,layout:"fit",autoShow:true,closeAction:"destroy",modal:true,iconCls:"icon-application_form",title:"Журнал документов",items:[]});