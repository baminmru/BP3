
Ext.require([
'Ext.form.*'
]);

function DefineInterface_bp3doc_store_(id,treestore_bp3doc_store){

var groupingFeature_bp3doc_field = Ext.create('Ext.grid.feature.Grouping',{
groupByText:  'Группировать по этому полю',
showGroupsText:  'Показать группировку'
});
var grid_bp3doc_field;
    function ChildOnDeleteConfirm(selection){
      if (selection) {
        Ext.Ajax.request({
            url:    rootURL+'index.php/c_bp3doc_field/deleteRow',
            method:  'POST',
    		params: { 
    				bp3doc_fieldid: selection.get('bp3doc_fieldid')
    				}
    	});
    	grid_bp3doc_field.store.remove(selection);
      }
    };
     function ChildOnDeleteClick(){
    if( grid_bp3doc_field.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
      var selection = grid_bp3doc_field.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	  if(CheckOperation('bp3doc.edit')!=0){
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
            caller: grid_bp3doc_field,
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
    if( grid_bp3doc_field.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
   	if(CheckOperation('bp3doc.edit')!=0){
                var edit = Ext.create('EditWindow_bp3doc_field');
                grid_bp3doc_field.store.insert(0, new model_bp3doc_field());
                record= grid_bp3doc_field.store.getAt(0);
                record.set('parentid',grid_bp3doc_field.parentid);
                edit.getComponent(0).setActiveRecord(record,grid_bp3doc_field.instanceid,grid_bp3doc_field.parentid);
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
        if( grid_bp3doc_field.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
            grid_bp3doc_field.store.load({params:{parentid: grid_bp3doc_field.parentid}});
    };
    function ChildOnEditClick(){
    if( grid_bp3doc_field.parentid=='{00000000-0000-0000-0000-000000000000}') {return;}
        var selection = grid_bp3doc_field.getView().getSelectionModel().getSelection()[0];
        if (selection) {
   	     if(CheckOperation('bp3doc.edit')!=0){
            var edit = Ext.create('EditWindow_bp3doc_field');
            edit.getComponent(0).setActiveRecord(selection,grid_bp3doc_field.instanceid,grid_bp3doc_field.parentid);
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
   grid_bp3doc_field=
    new Ext.grid.Panel({
        itemId:  'grd_bp3doc_field',
        minWidth: 200,
        maxWidth: 1700,
		height:535,
        iconCls:  'icon-grid',
        frame: true,
        parentid: '{00000000-0000-0000-0000-000000000000}',
        title: 'Поле',
        scroll:'both',
        store: {
        model:'model_bp3doc_field',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3doc_field/getRows',
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
        features: [groupingFeature_bp3doc_field],
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
{text: "№ п/п", width:60, dataIndex: 'sequence', sortable: true}
            ,
{text: "Имя поля", width: 200, dataIndex: 'name', sortable: true}
            ,
{text: "Надпись", width: 200, dataIndex: 'caption', sortable: true}
            ,
{text: "Имя вкладки", width: 200, dataIndex: 'tabname', sortable: true}
            ,
{text: "Имя группы", width: 200, dataIndex: 'fieldgroupbox', sortable: true}
            ,
{text: "Может быть пустым", width:80, dataIndex: 'allownull_grid', sortable: true}
            ,
{text: "Тип поля", width: 200, dataIndex: 'fieldtype_grid', sortable: true}
            ,
{text: "Тип ссылки", width:80, dataIndex: 'referencetype_grid', sortable: true}
            ,
{text: "Размер поля", width:60, dataIndex: 'datasize', sortable: true}
            ,
{text: "Ссылка на раздел", width: 200, dataIndex: 'reftopart_grid', sortable: true}
            ,
{text: "Ссылка в пределах объекта", width:80, dataIndex: 'internalreference_grid', sortable: true}
            ,
{text: "Описание", width: 200, dataIndex: 'thecomment', sortable: true}
            ,
{text: "Автонумерация", width:80, dataIndex: 'isautonumber_grid', sortable: true}
            ,
{text: "Краткая информация", width:80, dataIndex: 'isbrief_grid', sortable: true}
            ,
{text: "Для отображения в таблице", width:80, dataIndex: 'istabbrief_grid', sortable: true}
            ,
{text: "Стиль", width: 200, dataIndex: 'thestyle', sortable: true}
            ,
{text: "Маска", width: 200, dataIndex: 'themask', sortable: true}
            ,
{text: "Шаблон для краткого отображения", width: 200, dataIndex: 'shablonbrief', sortable: true}
        ],
    listeners: {
        itemdblclick: function() { 
    	    ChildOnEditClick();
        },
          itemclick: function(view , record){
         grid_bp3doc_field.down('#delete').setDisabled(false);
          grid_bp3doc_field.down('#edit').setDisabled(false);
    },
    select: function( selmodel, record,  index,  eOpts ){
        grid_bp3doc_field.down('#delete').setDisabled(false);
        grid_bp3doc_field.down('#edit').setDisabled(false);
    }, 
    selectionchange: function(selModel, selections){
    	 grid_bp3doc_field.down('#delete').setDisabled(selections.length === 0);
    	 grid_bp3doc_field.down('#edit').setDisabled(selections.length === 0);
    }
    }
    }
    );
var p1;
    function onDeleteConfirm (selection){
      if (selection) {
        Ext.Ajax.request({
            url:    rootURL+'index.php/c_bp3doc_store/deleteRow',
            method:  'POST',
    		params: { 
    				bp3doc_storeid: selection.get('bp3doc_storeid')
    				}
    	});
    	onRefreshClick();
      }
    };
    function onDeleteClick(){
      var selection = p1.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	    if(CheckOperation('bp3doc.edit')!=0){
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
     function onAddRootClick(){
   	if(CheckOperation('bp3doc.edit')!=0){
                var edit = Ext.create('EditWindow_bp3doc_store');
                p1.lasttreeid='{00000000-0000-0000-0000-000000000000}';
                record=new model_bp3doc_store();
                record.set('instanceid',p1.instanceid);
                record.set('parentrowid',p1.lasttreeid);
                p1.store.getRootNode().insertChild(0, record);
                edit.getComponent(0).setActiveRecord(p1,record,p1.instanceid);
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
    function onAddClick(){
   	if(CheckOperation('bp3doc.edit')!=0){
               var selection = p1.getView().getSelectionModel().getSelection()[0];
               if (selection) {
                    p1.lasttreeid=selection.get('bp3doc_storeid');
                    record=new model_bp3doc_store();
                    record.set('instanceid',p1.instanceid);
                    record.set('parentrowid',p1.lasttreeid);
                    p1.store.getNodeById(p1.lasttreeid).insertChild(0, record);
               }else{
                    p1.lasttreeid='{00000000-0000-0000-0000-000000000000}';
                    record=new model_bp3doc_store();
                    record.set('instanceid',p1.instanceid);
                    record.set('parentrowid',p1.lasttreeid);
                    p1.store.getRootNode().insertChild(0, record);
               }
                var edit = Ext.create('EditWindow_bp3doc_store');
                record.set('instanceid',p1.instanceid);
                record.set('parentrowid',p1.lasttreeid);
                edit.getComponent(0).setActiveRecord(p1,record,p1.instanceid);
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
   	if(CheckOperation('bp3doc.edit')!=0){
            var edit = Ext.create('EditWindow_bp3doc_store');
            edit.getComponent(0).setActiveRecord(p1,selection,selection.get('instanceid'));
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
p1=    new Ext.tree.Panel({
        itemId:id,
        autoScroll:true,
        width:805,
		height:535,
        frame: true,
        instanceid: '{00000000-0000-0000-0000-000000000000}',
        lasttreeid: '{00000000-0000-0000-0000-000000000000}',
        rootVisible:false,
        store: treestore_bp3doc_store,
          dockedItems: [{
                xtype:  'toolbar',
                items: [
                {
                    iconCls:  'icon-application_form_add',
                    text:   'Создать в корне',
                    scope:  this,
                    handler : onAddRootClick
                    }, 
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
{xtype: 'treecolumn',text: "№ п/п", width: 100, dataIndex: 'sequence', sortable: true}
            ,
{text: "Заголовок",width: 160, dataIndex: 'caption', sortable: true}
            ,
{text: "Тип структры", width: 60, dataIndex: 'parttype_grid', sortable: true}
            ,
{text: "Название",width: 160, dataIndex: 'name', sortable: true}
            ,
{text: "Описание",width: 160, dataIndex: 'the_comment', sortable: true}
            ,
{text: "Главный раздел", width: 60, dataIndex: 'isdocinstance_grid', sortable: true}
            ,
{text: "Архивировать", width: 60, dataIndex: 'usearchiving_grid', sortable: true}
            ,
{text: "Не  журналировать", width: 60, dataIndex: 'nolog_grid', sortable: true}
            ,
{text: "Шаблон Бриеф",width: 160, dataIndex: 'shablonbrief', sortable: true}
            ,
{text: "Вести журнал", width: 60, dataIndex: 'usechangelog_grid', sortable: true}
            ,
{text: "Правило  BRIEF",width: 160, dataIndex: 'rulebrief', sortable: true}
            ,
{text: "Иконка",width: 160, dataIndex: 'particoncls', sortable: true}
        ]
,
	rbar:grid_bp3doc_field 
       ,
        listeners: {
        resize: function ( tree, width, height, oldWidth, oldHeight, eOpts ){
        		grid_bp3doc_field.setWidth( width * 0.6 );
        },
        itemdblclick: function() { 
    	    onEditClick();
        }
        ,itemclick: function(view,record) { 
           p1.down('#delete').setDisabled(false);
           p1.down('#edit').setDisabled(false);
           grid_bp3doc_field.instanceid=p1.instanceid;
           grid_bp3doc_field.parentid=record.get('id');
           grid_bp3doc_field.store.load({params:{ parentid:record.get('id')} })
        },
        select: function(view,record) { 
           p1.down('#delete').setDisabled(false);
           p1.down('#edit').setDisabled(false);
           grid_bp3doc_field.instanceid=p1.instanceid;
           grid_bp3doc_field.parentid=record.get('id');
           grid_bp3doc_field.store.load({params:{ parentid:record.get('id')} })
        },
        selectionchange: function(selModel, selections){
        p1.down('#delete').setDisabled(selections.length === 0);
        p1.down('#edit').setDisabled(selections.length === 0);
        var selection = selections[0];
        if (selection) {
           grid_bp3doc_field.instanceid=p1.instanceid;
           grid_bp3doc_field.parentid=selection.get('id');
           grid_bp3doc_field.store.load({params:{ parentid:selection.get('id')} })
        }
       }
      }
    }
    );
return p1;
};
function DefineForms_bp3doc_store_(){


Ext.define('Form_bp3doc_store', {
extend:  'Ext.form.Panel',
alias: 'widget.f_bp3doc_store',
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
        id:'bp3doc_store-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 0, 

xtype:  'numberfield',
value:  '0',
name:   'sequence',
itemId:   'sequence',
fieldLabel:  '№ п/п',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 0, 

xtype:  'textfield',
value:  '',
name:   'caption',
itemId:   'caption',
fieldLabel:  'Заголовок',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 46, 

xtype:          'combobox',
editable: false,
store: enum_PartType,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'parttype_grid',
itemId:   'parttype_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('parttype', records[0].get('value'));}  },
fieldLabel:  'Тип структры',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 46, 

xtype:  'textfield',
value:  '',
name:   'name',
itemId:   'name',
fieldLabel:  'Название',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 740,
        width: 740,
        xtype: 'textarea', 
        x: 5, 
        y: 92, 
        height: 80, 

xtype:  'textarea',
value:  '',
name:   'the_comment',
itemId:   'the_comment',
fieldLabel:  'Описание',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 182, 

xtype:          'combobox',
editable: false,
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'isdocinstance_grid',
itemId:   'isdocinstance_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('isdocinstance', records[0].get('value'));}  },
fieldLabel:  'Главный раздел',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 182, 

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
name:   'usearchiving_grid',
itemId:   'usearchiving_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('usearchiving', records[0].get('value'));}  },
fieldLabel:  'Архивировать',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 228, 

xtype:          'combobox',
editable: false,
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'nolog_grid',
itemId:   'nolog_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('nolog', records[0].get('value'));}  },
fieldLabel:  'Не  журналировать',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 228, 

xtype:  'textfield',
value:  '',
name:   'shablonbrief',
itemId:   'shablonbrief',
fieldLabel:  'Шаблон Бриеф',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 274, 

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
name:   'usechangelog_grid',
itemId:   'usechangelog_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('usechangelog', records[0].get('value'));}  },
fieldLabel:  'Вести журнал',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 274, 

xtype:  'textfield',
value:  '',
name:   'rulebrief',
itemId:   'rulebrief',
fieldLabel:  'Правило  BRIEF',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 320, 

xtype:  'textfield',
value:  '',
name:   'particoncls',
itemId:   'particoncls',
fieldLabel:  'Иконка',
allowBlank:true
       ,labelWidth: 140
}
       ], width: 770,
       height: 396 
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
    setActiveRecord: function(tree,record,instid){
    this.tree=tree;
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
                url: rootURL+'index.php/c_bp3doc_store/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid
                    ,bp3doc_storeid: active.get('bp3doc_storeid')
                    ,treeid: active.get('parentrowid')
                    ,sequence: active.get('sequence') 
                    ,caption: active.get('caption') 
                    ,parttype: active.get('parttype') 
                    ,name: active.get('name') 
                    ,the_comment: active.get('the_comment') 
                    ,isdocinstance: active.get('isdocinstance') 
                    ,usearchiving: active.get('usearchiving') 
                    ,nolog: active.get('nolog') 
                    ,shablonbrief: active.get('shablonbrief') 
                    ,usechangelog: active.get('usechangelog') 
                    ,rulebrief: active.get('rulebrief') 
                    ,particoncls: active.get('particoncls') 
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
                    if(active.get('bp3doc_storeid')==''){
               			active.set('id',res.data['bp3doc_storeid']);
               			active.set('bp3doc_storeid',res.data['bp3doc_storeid']);
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
        if(this.activeRecord.get('bp3doc_storeid')==''){
        	ts =this.tree.store;
        	ts.getRootNode().removeAll();
        	ts.load();
        }
        this.setActiveRecord(null,null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_bp3doc_store', {
    extend:  'Ext.window.Window',
    maxHeight: 731,
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
    title:  'Раздел',
    items:[{
        xtype:  'f_bp3doc_store'
	}]
	});

Ext.define('Form_bp3doc_field', {
extend:  'Ext.form.Panel',
alias: 'widget.f_bp3doc_field',
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
        id:'bp3doc_field-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 0, 

xtype:  'numberfield',
value:  '0',
name:   'sequence',
itemId:   'sequence',
fieldLabel:  '№ п/п',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 0, 

xtype:  'textfield',
value:  '',
name:   'name',
itemId:   'name',
fieldLabel:  'Имя поля',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 46, 

xtype:  'textfield',
value:  '',
name:   'caption',
itemId:   'caption',
fieldLabel:  'Надпись',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 46, 

xtype:  'textfield',
value:  '',
name:   'tabname',
itemId:   'tabname',
fieldLabel:  'Имя вкладки',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 92, 

xtype:  'textfield',
value:  '',
name:   'fieldgroupbox',
itemId:   'fieldgroupbox',
fieldLabel:  'Имя группы',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 92, 

xtype:          'combobox',
editable: false,
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'allownull_grid',
itemId:   'allownull_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('allownull', records[0].get('value'));}  },
fieldLabel:  'Может быть пустым',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 138, 

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
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 138, 

xtype:          'combobox',
editable: false,
store: enum_ReferenceType,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'referencetype_grid',
itemId:   'referencetype_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('referencetype', records[0].get('value'));}  },
fieldLabel:  'Тип ссылки',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 184, 

xtype:  'numberfield',
value:  '0',
name:   'datasize',
itemId:   'datasize',
fieldLabel:  'Размер поля',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 184, 

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
fieldLabel:  'Ссылка на раздел',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
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
name:   'internalreference_grid',
itemId:   'internalreference_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('internalreference', records[0].get('value'));}  },
fieldLabel:  'Ссылка в пределах объекта',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 740,
        width: 740,
        xtype: 'textarea', 
        x: 5, 
        y: 276, 
        height: 80, 

xtype:  'textarea',
value:  '',
name:   'thecomment',
itemId:   'thecomment',
fieldLabel:  'Описание',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 366, 

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
name:   'isautonumber_grid',
itemId:   'isautonumber_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('isautonumber', records[0].get('value'));}  },
fieldLabel:  'Автонумерация',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 366, 

xtype:          'combobox',
editable: false,
store: enum_Boolean,
valueField:     'name',
displayField:   'name',
typeAhead: true,
queryMode:      'local',
emptyText:      '',
name:   'isbrief_grid',
itemId:   'isbrief_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('isbrief', records[0].get('value'));}  },
fieldLabel:  'Краткая информация',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 412, 

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
name:   'istabbrief_grid',
itemId:   'istabbrief_grid',
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('istabbrief', records[0].get('value'));}  },
fieldLabel:  'Для отображения в таблице',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 412, 

xtype:  'textfield',
value:  '',
name:   'thestyle',
itemId:   'thestyle',
fieldLabel:  'Стиль',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 5, 
        y: 458, 

xtype:  'textfield',
value:  '',
name:   'themask',
itemId:   'themask',
fieldLabel:  'Маска',
allowBlank:true
       ,labelWidth: 140
}
,
{
        minWidth: 370,
        width: 370,
        maxWidth: 370,
        x: 380, 
        y: 458, 

xtype:  'textfield',
value:  '',
name:   'shablonbrief',
itemId:   'shablonbrief',
fieldLabel:  'Шаблон для краткого отображения',
allowBlank:true
       ,labelWidth: 140
}
       ], width: 770,
       height: 534 
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
                url: rootURL+'index.php/c_bp3doc_field/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid,
                    parentid: this.parentid
                    ,bp3doc_fieldid: active.get('bp3doc_fieldid')
                    ,sequence: active.get('sequence') 
                    ,name: active.get('name') 
                    ,caption: active.get('caption') 
                    ,tabname: active.get('tabname') 
                    ,fieldgroupbox: active.get('fieldgroupbox') 
                    ,allownull: active.get('allownull') 
                    ,fieldtype: active.get('fieldtype') 
                    ,referencetype: active.get('referencetype') 
                    ,datasize: active.get('datasize') 
                    ,reftopart: active.get('reftopart') 
                    ,internalreference: active.get('internalreference') 
                    ,thecomment: active.get('thecomment') 
                    ,isautonumber: active.get('isautonumber') 
                    ,isbrief: active.get('isbrief') 
                    ,istabbrief: active.get('istabbrief') 
                    ,thestyle: active.get('thestyle') 
                    ,themask: active.get('themask') 
                    ,shablonbrief: active.get('shablonbrief') 
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
                    if(active.get('bp3doc_fieldid')==''){
               			active.set('bp3doc_fieldid',res.data['bp3doc_fieldid']);
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
        if(this.activeRecord.get('bp3doc_fieldid')==''){
                this.activeRecord.store.reload();
        }
        this.setActiveRecord(null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_bp3doc_field', {
    extend:  'Ext.window.Window',
    maxHeight: 639,
    maxWidth: 900,
    autoScroll:true,
    minWidth: 750,
    width: 800,
    minHeight:609,
    height:609,
    constrainHeader :true,
    layout:  'absolute',
    autoShow: true,
    modal: true,
    closable: false,
    closeAction: 'destroy',
    iconCls:  'icon-application_form',
    title:  'Поле',
    items:[{
        xtype:  'f_bp3doc_field'
	}]
	});
}