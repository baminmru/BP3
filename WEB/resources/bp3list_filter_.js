
Ext.require([
'Ext.form.*'
]);

function DefineInterface_bp3list_filter_(id,mystore){

var groupingFeature_bp3list_filterfield = Ext.create('Ext.grid.feature.Grouping',{
groupByText:  'Группировать по этому полю',
showGroupsText:  'Показать группировку'
});
var grid_bp3list_filterfield;
    function ChildOnDeleteConfirm(selection){
      if (selection) {
        Ext.Ajax.request({
            url:    rootURL+'index.php/c_bp3list_filterfield/deleteRow',
            method:  'POST',
    		params: { 
    				bp3list_filterfieldid: selection.get('bp3list_filterfieldid')
    				}
    	});
    	grid_bp3list_filterfield.store.remove(selection);
      }
    };
     function ChildOnDeleteClick(){
    if( grid_bp3list_filterfield.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
      var selection = grid_bp3list_filterfield.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	  if(CheckOperation('bp3list.edit')!=0){
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
            caller: grid_bp3list_filterfield,
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
    if( grid_bp3list_filterfield.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
   	if(CheckOperation('bp3list.edit')!=0){
                var edit = Ext.create('EditWindow_bp3list_filterfield');
                grid_bp3list_filterfield.store.insert(0, new model_bp3list_filterfield());
                record= grid_bp3list_filterfield.store.getAt(0);
                record.set('parentid',grid_bp3list_filterfield.parentid);
                edit.getComponent(0).setActiveRecord(record,grid_bp3list_filterfield.instanceid,grid_bp3list_filterfield.parentid);
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
        if( grid_bp3list_filterfield.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
            grid_bp3list_filterfield.store.load({params:{parentid: grid_bp3list_filterfield.parentid}});
    };
    function ChildOnEditClick(){
    if( grid_bp3list_filterfield.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
        var selection = grid_bp3list_filterfield.getView().getSelectionModel().getSelection()[0];
        if (selection) {
   	     if(CheckOperation('bp3list.edit')!=0){
            var edit = Ext.create('EditWindow_bp3list_filterfield');
            edit.getComponent(0).setActiveRecord(selection,grid_bp3list_filterfield.instanceid,grid_bp3list_filterfield.parentid);
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
   grid_bp3list_filterfield=
    new Ext.grid.Panel({
        itemId:  'grd_bp3list_filterfield',
        minHeight: 200,
        maxHeight: 1200,
        iconCls:  'icon-grid',
        frame: true,
        parentid: '{00000000-0000-0000-0000-000000000000}',
        title: 'Поле фильтра',
        scroll:'both',
        stateful:stateFulSystem,
        stateId:  'bp3list_filterfield',
        store: {
        model:'model_bp3list_filterfield',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3list_filterfield/getRows',
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
        features: [groupingFeature_bp3list_filterfield],
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
{text: "Размер", width:60, dataIndex: 'fieldsize', sortable: true}
            ,
{text: "Тип ссылки", width:80, dataIndex: 'reftype_grid', sortable: true}
            ,
{text: "Заголовок", width: 200, dataIndex: 'caption', sortable: true}
            ,
{text: "Название", width: 200, dataIndex: 'name', sortable: true}
            ,
{text: "Тип поля", width: 200, dataIndex: 'fieldtype_grid', sortable: true}
            ,
{text: "Массив значений", width:80, dataIndex: 'valuearray_grid', sortable: true}
            ,
{text: "Последовательность", width:60, dataIndex: 'sequence', sortable: true}
            ,
{text: "Раздел, куда ссылаемся", width: 200, dataIndex: 'reftopart_grid', sortable: true}
        ],
    listeners: {
        itemdblclick: function() { 
    	    ChildOnEditClick();
        },
          itemclick: function(view , record){
         grid_bp3list_filterfield.down('#delete').setDisabled(false);
          grid_bp3list_filterfield.down('#edit').setDisabled(false);
    },
    select: function( selmodel, record,  index,  eOpts ){
        grid_bp3list_filterfield.down('#delete').setDisabled(false);
        grid_bp3list_filterfield.down('#edit').setDisabled(false);
    }, 
    selectionchange: function(selModel, selections){
    	 grid_bp3list_filterfield.down('#delete').setDisabled(selections.length === 0);
    	 grid_bp3list_filterfield.down('#edit').setDisabled(selections.length === 0);
    }
    }
    }
    );
var groupingFeature_bp3list_filter = Ext.create('Ext.grid.feature.Grouping',{
groupByText:  'Группировать по этому полю',
showGroupsText:  'Показать группировку'
});
var p1;
    function onDeleteConfirm(selection){
      if (selection) {
        Ext.Ajax.request({
            url:    rootURL+'index.php/c_bp3list_filter/deleteRow',
            method:  'POST',
    		params: { 
    				bp3list_filterid: selection.get('bp3list_filterid')
    				}
    	});
    	p1.store.remove(selection);
      }
    };
    function onDeleteClick(){
      var selection = p1.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	  if(CheckOperation('bp3list.edit')!=0){
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
   	if(CheckOperation('bp3list.edit')!=0){
                var edit = Ext.create('EditWindow_bp3list_filter');
                p1.store.insert(0, new model_bp3list_filter());
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
   	    if(CheckOperation('bp3list.edit')!=0){
            var edit = Ext.create('EditWindow_bp3list_filter');
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
        features: [groupingFeature_bp3list_filter],
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
{text: "Заголовок", width: 200, dataIndex: 'caption', sortable: true }
            ,
{text: "Последовательность", width:60, dataIndex: 'sequence', sortable: true}
            ,
{text: "Название", width: 200, dataIndex: 'name', sortable: true }
            ,
{text: "Можно отключать", width:60, dataIndex: 'allowignore_grid', sortable: true}
        ]
,
	bbar:grid_bp3list_filterfield, 
    listeners: {
        resize: function ( tree, width, height, oldWidth, oldHeight, eOpts ){
        		grid_bp3list_filterfield.setHeight( height * 0.5 );
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
           grid_bp3list_filterfield.instanceid=p1.instanceid;
           grid_bp3list_filterfield.parentid=record.get('bp3list_filterid');
           grid_bp3list_filterfield.store.load({params:{ parentid:record.get('bp3list_filterid')} })
        },
        selectionchange: function(selModel, selections){
        p1.down('#delete').setDisabled(selections.length === 0);
        p1.down('#edit').setDisabled(selections.length === 0);
        var selection = selections[0];
        if (selection) {
           p1.down('#grd_bp3list_filterfield').instanceid=p1.instanceid;
           p1.down('#grd_bp3list_filterfield').parentid=selection.get('bp3list_filterid');
           p1.down('#grd_bp3list_filterfield').store.load({params:{ parentid:selection.get('bp3list_filterid')} })
        }
       }
    }
    }
    );
return p1;
};
function DefineForms_bp3list_filter_(){


Ext.define('Form_bp3list_filter', {
extend:  'Ext.form.Panel',
alias: 'widget.f_bp3list_filter',
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
        id:'bp3list_filter-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 0, 

xtype:  'textfield',
value:  '',
name:   'caption',
itemId:   'caption',
fieldLabel:  'Заголовок',
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

xtype:  'numberfield',
value:  '0',
name:   'sequence',
itemId:   'sequence',
fieldLabel:  'Последовательность',
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
fieldLabel:  'Название',
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

xtype:          'combobox',
editable: false,
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'allowignore_grid',
itemId:   'allowignore_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('allowignore', records[0].get('value'));}  },
fieldLabel:  'Можно отключать',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
       ], width: 770,
       height: 214 
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
                url: rootURL+'index.php/c_bp3list_filter/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid
                    ,bp3list_filterid: active.get('bp3list_filterid')
                    ,caption: active.get('caption') 
                    ,sequence: active.get('sequence') 
                    ,name: active.get('name') 
                    ,allowignore: active.get('allowignore') 
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
                    if(active.get('bp3list_filterid')==''){
               			active.set('bp3list_filterid',res.data['bp3list_filterid']);
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
        if(this.activeRecord.get('bp3list_filterid')==''){
                this.activeRecord.store.reload();
        }
        this.setActiveRecord(null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_bp3list_filter', {
    extend:  'Ext.window.Window',
    maxHeight: 319,
    maxWidth: 900,
    autoScroll:true,
    minWidth: 750,
    width: 800,
    minHeight:289,
    height:289,
    constrainHeader :true,
    layout:  'absolute',
    autoShow: true,
    modal: true,
    closable: false,
    closeAction: 'destroy',
    iconCls:  'icon-application_form',
    title:  'Группа полей фильтра',
    items:[{
        xtype:  'f_bp3list_filter'
	}]
	});

Ext.define('Form_bp3list_filterfield', {
extend:  'Ext.form.Panel',
alias: 'widget.f_bp3list_filterfield',
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
        id:'bp3list_filterfield-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 0, 

xtype:  'numberfield',
value:  '0',
name:   'fieldsize',
itemId:   'fieldsize',
fieldLabel:  'Размер',
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

xtype:          'combobox',
editable: false,
trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
},
onTrigger2Click : function(){ 
		if(this.isExpanded) {
			this.collapse(); 
		}else{ 
			this.expand();
		}
},
store: enum_ReferenceType,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'reftype_grid',
itemId:   'reftype_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('reftype', records[0].get('value'));}  },
fieldLabel:  'Тип ссылки',
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
fieldLabel:  'Заголовок',
allowBlank:true
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
fieldLabel:  'Название',
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
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('fieldtype', records[0].get('id'));}  },
store: cmbstore_bp3ft_def,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'fieldtype_grid',
itemId:   'fieldtype_grid',
fieldLabel:  'Тип поля',
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
        y: 230, 

xtype:          'combobox',
editable: false,
trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
},
onTrigger2Click : function(){ 
		if(this.isExpanded) {
			this.collapse(); 
		}else{ 
			this.expand();
		}
},
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'valuearray_grid',
itemId:   'valuearray_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('valuearray', records[0].get('value'));}  },
fieldLabel:  'Массив значений',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 740,
        width: 740,
        maxWidth: 740,
        x: 5, 
        y: 276, 

xtype:  'numberfield',
value:  '0',
name:   'sequence',
itemId:   'sequence',
fieldLabel:  'Последовательность',
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
        y: 322, 

xtype:  'combobox',
trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
		this.up('form' ).activeRecord.set('reftopart',null );
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
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('reftopart', records[0].get('id'));}  },
store: cmbstore_bp3doc_store,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'reftopart_grid',
itemId:   'reftopart_grid',
fieldLabel:  'Раздел, куда ссылаемся',
allowBlank:true
       ,labelWidth: 120
}
       ], width: 770,
       height: 398 
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
                url: rootURL+'index.php/c_bp3list_filterfield/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid,
                    parentid: this.parentid
                    ,bp3list_filterfieldid: active.get('bp3list_filterfieldid')
                    ,fieldsize: active.get('fieldsize') 
                    ,reftype: active.get('reftype') 
                    ,caption: active.get('caption') 
                    ,name: active.get('name') 
                    ,fieldtype: active.get('fieldtype') 
                    ,valuearray: active.get('valuearray') 
                    ,sequence: active.get('sequence') 
                    ,reftopart: active.get('reftopart') 
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
                    if(active.get('bp3list_filterfieldid')==''){
               			active.set('bp3list_filterfieldid',res.data['bp3list_filterfieldid']);
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
        if(this.activeRecord.get('bp3list_filterfieldid')==''){
                this.activeRecord.store.reload();
        }
        this.setActiveRecord(null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_bp3list_filterfield', {
    extend:  'Ext.window.Window',
    maxHeight: 503,
    maxWidth: 900,
    autoScroll:true,
    minWidth: 750,
    width: 800,
    minHeight:473,
    height:473,
    constrainHeader :true,
    layout:  'absolute',
    autoShow: true,
    modal: true,
    closable: false,
    closeAction: 'destroy',
    iconCls:  'icon-application_form',
    title:  'Поле фильтра',
    items:[{
        xtype:  'f_bp3list_filterfield'
	}]
	});
}