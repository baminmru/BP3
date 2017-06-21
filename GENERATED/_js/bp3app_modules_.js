
Ext.require([
'Ext.form.*'
]);

function DefineInterface_bp3app_modules_(id,mystore){

var groupingFeature_bp3app_oper = Ext.create('Ext.grid.feature.Grouping',{
groupByText:  'Группировать по этому полю',
showGroupsText:  'Показать группировку'
});
var grid_bp3app_oper;
    function ChildOnDeleteConfirm(selection){
      if (selection) {
        Ext.Ajax.request({
            url:    rootURL+'index.php/c_bp3app_oper/deleteRow',
            method:  'POST',
    		params: { 
    				bp3app_operid: selection.get('bp3app_operid')
    				}
    	});
    	grid_bp3app_oper.store.remove(selection);
      }
    };
     function ChildOnDeleteClick(){
    if( grid_bp3app_oper.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
      var selection = grid_bp3app_oper.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	  if(CheckOperation('bp3app.edit')!=0){
        Ext.Msg.show({
            title:  'Удалить?',
            msg:    'Удалить строку из базы данных?',
        	buttons: Ext.Msg.YESNO,
            icon:   Ext.window.MessageBox.QUESTION,
        	fn: function(btn,text,opt){
        		if(btn=='yes'){
        			ChildOnDeleteConfirm(opt.selectedRow);
        	    }
        	},
            caller: grid_bp3app_oper,
            selectedRow: selection
        });
        }else{
        		Ext.MessageBox.show({
                title:  'Контроль прав.',
                msg:    'Удаление строк не разрешено!',
                buttons: Ext.MessageBox.OK,
               icon:   Ext.MessageBox.WARNING
        		});
        }
      }
    };
    function ChildOnAddClick(){
    if( grid_bp3app_oper.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
   	if(CheckOperation('bp3app.edit')!=0){
                var edit = Ext.create('EditWindow_bp3app_oper');
                grid_bp3app_oper.store.insert(0, new model_bp3app_oper());
                record= grid_bp3app_oper.store.getAt(0);
                record.set('parentid',grid_bp3app_oper.parentid);
                edit.getComponent(0).setActiveRecord(record,grid_bp3app_oper.instanceid,grid_bp3app_oper.parentid);
                edit.show();
        }else{
        		Ext.MessageBox.show({
                title:  'Контроль прав.',
                msg:    'Создание строк не разрешено!',
                buttons: Ext.MessageBox.OK,
               icon:   Ext.MessageBox.WARNING
        		});
        }
    };
     function ChildOnRefreshClick(){
        if( grid_bp3app_oper.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
            grid_bp3app_oper.store.load({params:{parentid: grid_bp3app_oper.parentid}});
    };
    function ChildOnEditClick(){
    if( grid_bp3app_oper.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
        var selection = grid_bp3app_oper.getView().getSelectionModel().getSelection()[0];
        if (selection) {
   	     if(CheckOperation('bp3app.edit')!=0){
            var edit = Ext.create('EditWindow_bp3app_oper');
            edit.getComponent(0).setActiveRecord(selection,grid_bp3app_oper.instanceid,grid_bp3app_oper.parentid);
            edit.show();
        }else{
        		Ext.MessageBox.show({
                title:  'Контроль прав.',
                msg:    'Изменение строк не разрешено!',
                buttons: Ext.MessageBox.OK,
               icon:   Ext.MessageBox.WARNING
        		});
        }
        }
    };
   grid_bp3app_oper=
    new Ext.grid.Panel({
        itemId:  'grd_bp3app_oper',
        minHeight: 200,
        maxHeight: 1200,
        iconCls:  'icon-grid',
        frame: true,
        parentid: '{00000000-0000-0000-0000-000000000000}',
        title: 'Действия и отчеты',
        scroll:'both',
        stateful:stateFulSystem,
        stateId:  'bp3app_oper',
        store: {
        model:'model_bp3app_oper',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3app_oper/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            },
            listeners: {
                exception: function(proxy, response, operation){
                }
            }
        }
    },
        features: [groupingFeature_bp3app_oper],
          dockedItems: [{
                xtype:  'toolbar',
                items: [
                {
                    iconCls:  'icon-application_form_add',
                    text:   'Создать',
                    scope:  this,
                    handler : ChildOnAddClick
                    }, {
                    iconCls:  'icon-application_form_edit',
                    text:   'Изменить',
                    scope:  this,
                    disabled: true,
                    itemId:  'edit',
                    handler : ChildOnEditClick
                    }, {
                    iconCls:  'icon-application_form_delete',
                    text:   'Удалить',
                    disabled: true,
                    itemId:  'delete',
                    scope:  this,
                    handler : ChildOnDeleteClick
                    }, {
                    iconCls:  'icon-table_refresh',
                    text:   'Обновить',
                    itemId:  'bRefresh',
                    scope:  this,
                    handler : ChildOnRefreshClick
                }]
            }],
        columns: [
{text: "Тип права", width: 200, dataIndex: 'righttype_grid', sortable: true}
            ,
{text: "Надпись", width: 200, dataIndex: 'caption', sortable: true}
            ,
{text: "Код", width: 200, dataIndex: 'name', sortable: true}
            ,
{text: "№ п/п", width:60, dataIndex: 'sequence', sortable: true}
            ,
{text: "Иконка", width: 200, dataIndex: 'theicon', sortable: true}
            ,
{text: "Это отчет", width:80, dataIndex: 'isreport_grid', sortable: true}
        ],
    listeners: {
        itemdblclick: function() { 
    	    ChildOnEditClick();
        },
          itemclick: function(view , record){
         grid_bp3app_oper.down('#delete').setDisabled(false);
          grid_bp3app_oper.down('#edit').setDisabled(false);
    },
    select: function( selmodel, record,  index,  eOpts ){
        grid_bp3app_oper.down('#delete').setDisabled(false);
        grid_bp3app_oper.down('#edit').setDisabled(false);
    }, 
    selectionchange: function(selModel, selections){
    	 grid_bp3app_oper.down('#delete').setDisabled(selections.length === 0);
    	 grid_bp3app_oper.down('#edit').setDisabled(selections.length === 0);
    }
    }
    }
    );
var groupingFeature_bp3app_modules = Ext.create('Ext.grid.feature.Grouping',{
groupByText:  'Группировать по этому полю',
showGroupsText:  'Показать группировку'
});
var p1;
    function onDeleteConfirm(selection){
      if (selection) {
        Ext.Ajax.request({
            url:    rootURL+'index.php/c_bp3app_modules/deleteRow',
            method:  'POST',
    		params: { 
    				bp3app_modulesid: selection.get('bp3app_modulesid')
    				}
    	});
    	p1.store.remove(selection);
      }
    };
    function onDeleteClick(){
      var selection = p1.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	  if(CheckOperation('bp3app.edit')!=0){
        Ext.Msg.show({
            title:  'Удалить?',
            msg:    'Удалить строку из базы данных?',
        	buttons: Ext.Msg.YESNO,
            icon:   Ext.window.MessageBox.QUESTION,
        	fn: function(btn,text,opt){
        		if(btn=='yes'){
        			onDeleteConfirm(opt.selectedRow);
        	    }
        	},
            caller: this,
            selectedRow: selection
        });
        }else{
        		Ext.MessageBox.show({
                title:  'Контроль прав.',
                msg:    'Удаление строк не разрешено!',
                buttons: Ext.MessageBox.OK,
               icon:   Ext.MessageBox.WARNING
        		});
        }
      }
    };
    function onAddClick(){
   	if(CheckOperation('bp3app.edit')!=0){
                var edit = Ext.create('EditWindow_bp3app_modules');
                p1.store.insert(0, new model_bp3app_modules());
                record= p1.store.getAt(0);
                record.set('instanceid',p1.instanceid);
                edit.getComponent(0).setActiveRecord(record,p1.instanceid);
                edit.show();
        }else{
        		Ext.MessageBox.show({
                title:  'Контроль прав.',
                msg:    'Создание строк не разрешено!',
                buttons: Ext.MessageBox.OK,
               icon:   Ext.MessageBox.WARNING
        		});
        }
    };
    function onRefreshClick(){
            p1.store.load({params:{instanceid: p1.instanceid}});
    };
    function onEditClick(){
        var selection = p1.getView().getSelectionModel().getSelection()[0];
        if (selection) {
   	    if(CheckOperation('bp3app.edit')!=0){
            var edit = Ext.create('EditWindow_bp3app_modules');
            edit.getComponent(0).setActiveRecord(selection,selection.get('instanceid'));
            edit.show();
        }else{
        		Ext.MessageBox.show({
                title:  'Контроль прав.',
                msg:    'Изменение строк не разрешено!',
                buttons: Ext.MessageBox.OK,
               icon:   Ext.MessageBox.WARNING
        		});
        }
        }
    };
 p1=   new Ext.grid.Panel({
        itemId: id,
        store:  mystore,
        frame: true,
        instanceid: '',
        scroll:'both',
        autoScroll:true,
        width:600,
        features: [groupingFeature_bp3app_modules],
          dockedItems: [{
                xtype:  'toolbar',
                items: [
                {
                    iconCls:  'icon-application_form_add',
                    text:   'Создать',
                    scope:  this,
                    handler : onAddClick
                    }, {
                    iconCls:  'icon-application_form_edit',
                    text:   'Изменить',
                    scope:  this,
                    disabled: true,
                    itemId:  'edit',
                    handler : onEditClick
                    }, {
                    iconCls:  'icon-application_form_delete',
                    text:   'Удалить',
                    disabled: true,
                    itemId:  'delete',
                    scope:  this,
                    handler : onDeleteClick
                    }, {
                    iconCls:  'icon-table_refresh',
                    text:   'Обновить',
                    itemId:  'bRefresh',
                    scope:  this,
                    handler : onRefreshClick
                }]
            }],
        columns: [
{text: "Меню", width:200, dataIndex: 'topmenu_grid', sortable: true}
            ,
{text: "Имя группы", width: 200, dataIndex: 'groupname', sortable: true }
            ,
{text: "Надпись", width: 200, dataIndex: 'caption', sortable: true }
            ,
{text: "Код модуля", width: 200, dataIndex: 'name', sortable: true }
            ,
{text: "№ п/п", width:60, dataIndex: 'sequence', sortable: true}
            ,
{text: "Описание", width: 200, dataIndex: 'thecomment', sortable: true }
            ,
{text: "Иконка", width: 200, dataIndex: 'theicon', sortable: true }
            ,
{text: "Настраивать видимость", width:60, dataIndex: 'customizevisibility_grid', sortable: true}
            ,
{text: "Журнал", width:200, dataIndex: 'journal_grid', sortable: true}
            ,
{text: "ID Документа", width: 200, dataIndex: 'document', sortable: true }
            ,
{text: "Вариант действия", width:60, dataIndex: 'actiontype_grid', sortable: true}
            ,
{text: "Тип документа", width:200, dataIndex: 'objecttype_grid', sortable: true}
            ,
{text: "Отчет", width:200, dataIndex: 'report_grid', sortable: true}
        ]
,
	bbar:grid_bp3app_oper, 
    listeners: {
        resize: function ( tree, width, height, oldWidth, oldHeight, eOpts ){
        		grid_bp3app_oper.setHeight( height * 0.5 );
        },
        render : function(grid){
                grid.store.on('load', function(store, records, options){
                        if(store.count() > 0) grid.getSelectionModel().select(0);      
                }); 
         },
        itemdblclick: function() { 
    	    onEditClick();
        }
        ,itemclick: function(view,record) { 
           p1.down('#delete').setDisabled(false);
           p1.down('#edit').setDisabled(false);
           grid_bp3app_oper.instanceid=p1.instanceid;
           grid_bp3app_oper.parentid=record.get('bp3app_modulesid');
           grid_bp3app_oper.store.load({params:{ parentid:record.get('bp3app_modulesid')} })
        },
        selectionchange: function(selModel, selections){
        p1.down('#delete').setDisabled(selections.length === 0);
        p1.down('#edit').setDisabled(selections.length === 0);
        var selection = selections[0];
        if (selection) {
           p1.down('#grd_bp3app_oper').instanceid=p1.instanceid;
           p1.down('#grd_bp3app_oper').parentid=selection.get('bp3app_modulesid');
           p1.down('#grd_bp3app_oper').store.load({params:{ parentid:selection.get('bp3app_modulesid')} })
        }
       }
    }
    }
    );
return p1;
};
function DefineForms_bp3app_modules_(){


Ext.define('Form_bp3app_modules', {
extend:  'Ext.form.Panel',
alias: 'widget.f_bp3app_modules',
initComponent: function(){
    this.addEvents('create');
    Ext.apply(this,{
        activeRecord: null,
        defaultType:  'textfield',
        x: 0, 
        fieldDefaults: {
         labelAlign:  'top' //,
        },
        items: [
        { 
        xtype:'panel', 
        closable:false,
        title:'',
        preventHeader:true,
        id:'bp3app_modules-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 0, 

xtype:  'combobox',
trigger1Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
onTrigger1Click : function(){ 
		if(this.isExpanded) {
			this.collapse(); 
		}else{ 
			if(this.store.count(false)==0) this.store.load();
			this.expand();
		}
},
editable: false,
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('topmenu', records[0].get('id'));}  },
store: cmbstore_bp3app_menu,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'topmenu_grid',
itemId:   'topmenu_grid',
fieldLabel:  'Меню',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 46, 

xtype:  'textfield',
value:  '',
name:   'groupname',
itemId:   'groupname',
fieldLabel:  'Имя группы',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 92, 

xtype:  'textfield',
value:  '',
name:   'caption',
itemId:   'caption',
fieldLabel:  'Надпись',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 138, 

xtype:  'textfield',
value:  '',
name:   'name',
itemId:   'name',
fieldLabel:  'Код модуля',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 184, 

xtype:  'numberfield',
value:  '0',
name:   'sequence',
itemId:   'sequence',
fieldLabel:  '№ п/п',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        xtype: 'textarea', 
        x: 5, 
        y: 230, 
        height: 80, 

xtype:  'textarea',
value:  '',
name:   'thecomment',
itemId:   'thecomment',
fieldLabel:  'Описание',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 320, 

xtype:  'textfield',
value:  '',
name:   'theicon',
itemId:   'theicon',
fieldLabel:  'Иконка',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 366, 

xtype:          'combobox',
editable: false,
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'customizevisibility_grid',
itemId:   'customizevisibility_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('customizevisibility', records[0].get('value'));}  },
fieldLabel:  'Настраивать видимость',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 412, 

xtype:  'combobox',
trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
		this.up('form' ).activeRecord.set('journal',null );
},
onTrigger2Click : function(){ 
		if(this.isExpanded) {
			this.collapse(); 
		}else{ 
			if(this.store.count(false)==0) this.store.load();
			this.expand();
		}
},
editable: false,
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('journal', records[0].get('id'));}  },
store: cmbstore_bp3list_def,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'journal_grid',
itemId:   'journal_grid',
fieldLabel:  'Журнал',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 458, 

xtype:  'textfield',
value:  '',
name:   'document',
itemId:   'document',
fieldLabel:  'ID Документа',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 504, 

xtype:          'combobox',
editable: false,
store: enum_MenuActionType,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'actiontype_grid',
itemId:   'actiontype_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('actiontype', records[0].get('value'));}  },
fieldLabel:  'Вариант действия',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 550, 

xtype:  'combobox',
trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
		this.up('form' ).activeRecord.set('objecttype',null );
},
onTrigger2Click : function(){ 
		if(this.isExpanded) {
			this.collapse(); 
		}else{ 
			if(this.store.count(false)==0) this.store.load();
			this.expand();
		}
},
editable: false,
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('objecttype', records[0].get('id'));}  },
store: cmbstore_bp3doc_def,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'objecttype_grid',
itemId:   'objecttype_grid',
fieldLabel:  'Тип документа',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 596, 

xtype:  'combobox',
trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
		this.up('form' ).activeRecord.set('report',null );
},
onTrigger2Click : function(){ 
		if(this.isExpanded) {
			this.collapse(); 
		}else{ 
			if(this.store.count(false)==0) this.store.load();
			this.expand();
		}
},
editable: false,
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('report', records[0].get('id'));}  },
store: cmbstore_bp3report_def,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'report_grid',
itemId:   'report_grid',
fieldLabel:  'Отчет',
allowBlank:true
       ,labelWidth: 120
}
       ], width: 770,
       height: 672 
        }
          ],//items = part panel
        instanceid:'',
        dockedItems: [{
            xtype:  'toolbar',
            dock:   'bottom',
            ui:     'footer',
                items: ['->', {
                    iconCls:  'icon-accept',
                    itemId:  'save',
                    text:   'Сохранить',
                    disabled: true,
                    scope:  this,
                    handler : this.onSave
                }
               , {
                    iconCls:  'icon-cancel',
                    text:   'Закрыть',
                    scope:  this,
                    handler : this.onReset
                }
              ]
            }] // dockedItems
        }); //Ext.apply
        this.callParent();
    }, //initComponent 
    setActiveRecord: function(record,instid){
        this.activeRecord = record;
        this.instanceid = instid;
        if (record) {
            this.down('#save').enable();
            this.getForm().loadRecord(record);
        } else {
            this.down('#save').disable();
            this.getForm().reset();
        }
    },
    onSave: function(){
        var active = this.activeRecord,
            form = this.getForm();
        if (!active) {
            return;
        }
        if (form.isValid()) {
            form.updateRecord(active);
            // combobox patch
            // var form_values = form.getValues(); var field_name = '';  for(field_name in form_values){active.set(field_name, form_values[field_name]);}
        	StatusDB('Сохранение данных');
            Ext.Ajax.request({
                url: rootURL+'index.php/c_bp3app_modules/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid
                    ,bp3app_modulesid: active.get('bp3app_modulesid')
                    ,topmenu: active.get('topmenu') 
                    ,groupname: active.get('groupname') 
                    ,caption: active.get('caption') 
                    ,name: active.get('name') 
                    ,sequence: active.get('sequence') 
                    ,thecomment: active.get('thecomment') 
                    ,theicon: active.get('theicon') 
                    ,customizevisibility: active.get('customizevisibility') 
                    ,journal: active.get('journal') 
                    ,document: active.get('document') 
                    ,actiontype: active.get('actiontype') 
                    ,objecttype: active.get('objecttype') 
                    ,report: active.get('report') 
                }
                , success: function(response){
                var text = response.responseText;
                var res =Ext.decode(text);
	            if(res.success==false){
	       	        Ext.MessageBox.show({
	       		        title:  'Ошибка',
	       		        msg:    res.msg,
	       		        buttons: Ext.MessageBox.OK,
	       		        icon:   Ext.MessageBox.ERROR
	       	            });
        		    StatusErr( 'Ошибка. '+res.msg);
	            }else{
                    if(active.get('bp3app_modulesid')==''){
               			active.set('bp3app_modulesid',res.data['bp3app_modulesid']);
                    }
        		    StatusReady('Изменения сохранены');
                form.owner.ownerCt.close();
                }
              }
            });
        }else{
        		Ext.MessageBox.show({
                title:  'Ошибка',
                msg:    'Не все обязательные поля заполнены!',
                buttons: Ext.MessageBox.OK,
                icon:   Ext.MessageBox.ERROR
        		});
        }
    },
    onReset: function(){
        if(this.activeRecord.get('bp3app_modulesid')==''){
                this.activeRecord.store.reload();
        }
        this.setActiveRecord(null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_bp3app_modules', {
    extend:  'Ext.window.Window',
    maxHeight: 777,
    maxWidth: 900,
    autoScroll:true,
    minWidth: 750,
    width: 800,
    minHeight:670,
    height:670,
    constrainHeader :true,
    layout:  'absolute',
    autoShow: true,
    modal: true,
    closable: false,
    closeAction: 'destroy',
    iconCls:  'icon-application_form',
    title:  'Модули',
    items:[{
        xtype:  'f_bp3app_modules'
	}]
	});

Ext.define('Form_bp3app_oper', {
extend:  'Ext.form.Panel',
alias: 'widget.f_bp3app_oper',
initComponent: function(){
    this.addEvents('create');
    Ext.apply(this,{
        activeRecord: null,
        defaultType:  'textfield',
        x: 0, 
        fieldDefaults: {
         labelAlign:  'top' //,
        },
        items: [
        { 
        xtype:'panel', 
        closable:false,
        title:'',
        preventHeader:true,
        id:'bp3app_oper-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 0, 

xtype:  'combobox',
trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
		this.up('form' ).activeRecord.set('righttype',null );
},
onTrigger2Click : function(){ 
		if(this.isExpanded) {
			this.collapse(); 
		}else{ 
			if(this.store.count(false)==0) this.store.load();
			this.expand();
		}
},
editable: false,
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('righttype', records[0].get('id'));}  },
store: cmbstore_bp3app_rigthtype,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'righttype_grid',
itemId:   'righttype_grid',
fieldLabel:  'Тип права',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 46, 

xtype:  'textfield',
value:  '',
name:   'caption',
itemId:   'caption',
fieldLabel:  'Надпись',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 92, 

xtype:  'textfield',
value:  '',
name:   'name',
itemId:   'name',
fieldLabel:  'Код',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 138, 

xtype:  'numberfield',
value:  '0',
name:   'sequence',
itemId:   'sequence',
fieldLabel:  '№ п/п',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 184, 

xtype:  'textfield',
value:  '',
name:   'theicon',
itemId:   'theicon',
fieldLabel:  'Иконка',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 230, 

xtype:          'combobox',
editable: false,
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'isreport_grid',
itemId:   'isreport_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('isreport', records[0].get('value'));}  },
fieldLabel:  'Это отчет',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
       ], width: 770,
       height: 306 
        }
          ],//items = part panel
        instanceid:'',
        dockedItems: [{
            xtype:  'toolbar',
            dock:   'bottom',
            ui:     'footer',
                items: ['->', {
                    iconCls:  'icon-accept',
                    itemId:  'save',
                    text:   'Сохранить',
                    disabled: true,
                    scope:  this,
                    handler : this.onSave
                }
               , {
                    iconCls:  'icon-cancel',
                    text:   'Закрыть',
                    scope:  this,
                    handler : this.onReset
                }
              ]
            }] // dockedItems
        }); //Ext.apply
        this.callParent();
    }, //initComponent 
    setActiveRecord: function(record,instid,parentid){
        this.activeRecord = record;
        this.instanceid = instid;
        this.parentid = parentid;
        if (record) {
            this.down('#save').enable();
            this.getForm().loadRecord(record);
        } else {
            this.down('#save').disable();
            this.getForm().reset();
        }
    },
    onSave: function(){
        var active = this.activeRecord,
            form = this.getForm();
        if (!active) {
            return;
        }
        if (form.isValid()) {
            form.updateRecord(active);
            // combobox patch
            // var form_values = form.getValues(); var field_name = '';  for(field_name in form_values){active.set(field_name, form_values[field_name]);}
        	StatusDB('Сохранение данных');
            Ext.Ajax.request({
                url: rootURL+'index.php/c_bp3app_oper/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid,
                    parentid: this.parentid
                    ,bp3app_operid: active.get('bp3app_operid')
                    ,righttype: active.get('righttype') 
                    ,caption: active.get('caption') 
                    ,name: active.get('name') 
                    ,sequence: active.get('sequence') 
                    ,theicon: active.get('theicon') 
                    ,isreport: active.get('isreport') 
                }
                , success: function(response){
                var text = response.responseText;
                var res =Ext.decode(text);
	            if(res.success==false){
	       	        Ext.MessageBox.show({
	       		        title:  'Ошибка',
	       		        msg:    res.msg,
	       		        buttons: Ext.MessageBox.OK,
	       		        icon:   Ext.MessageBox.ERROR
	       	            });
        		    StatusErr( 'Ошибка. '+res.msg);
	            }else{
                    if(active.get('bp3app_operid')==''){
               			active.set('bp3app_operid',res.data['bp3app_operid']);
                    }
        		    StatusReady('Изменения сохранены');
                form.owner.ownerCt.close();
                }
              }
            });
        }else{
        		Ext.MessageBox.show({
                title:  'Ошибка',
                msg:    'Не все обязательные поля заполнены!',
                buttons: Ext.MessageBox.OK,
                icon:   Ext.MessageBox.ERROR
        		});
        }
    },
    onReset: function(){
        if(this.activeRecord.get('bp3app_operid')==''){
                this.activeRecord.store.reload();
        }
        this.setActiveRecord(null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_bp3app_oper', {
    extend:  'Ext.window.Window',
    maxHeight: 411,
    maxWidth: 900,
    autoScroll:true,
    minWidth: 750,
    width: 800,
    minHeight:381,
    height:381,
    constrainHeader :true,
    layout:  'absolute',
    autoShow: true,
    modal: true,
    closable: false,
    closeAction: 'destroy',
    iconCls:  'icon-application_form',
    title:  'Действия и отчеты',
    items:[{
        xtype:  'f_bp3app_oper'
	}]
	});
}