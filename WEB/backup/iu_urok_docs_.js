
Ext.require([
'Ext.form.*'
]);

function DefineInterface_iu_urok_docs_(id,mystore){

var groupingFeature_iu_urok_comments = Ext.create('Ext.grid.feature.Grouping',{
groupByText:  'Группировать по этому полю',
showGroupsText:  'Показать группировку'
});
var grid_iu_urok_comments;
    function ChildOnDeleteConfirm(selection){
      if (selection) {
        Ext.Ajax.request({
            url:    'index.php/c_iu_urok_comments/deleteRow',
            method:  'POST',
    		params: { 
    				iu_urok_commentsid: selection.get('iu_urok_commentsid')
    				}
    	});
    	grid_iu_urok_comments.store.remove(selection);
      }
    };
     function ChildOnDeleteClick(){
      var selection = grid_iu_urok_comments.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	  if(CheckOperation('iu_urok.edit')!=0){
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
            caller: grid_iu_urok_comments,
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
   	if(CheckOperation('iu_urok.edit')!=0){
                var edit = Ext.create('EditWindow_iu_urok_comments');
                grid_iu_urok_comments.store.insert(0, new model_iu_urok_comments());
                record= grid_iu_urok_comments.store.getAt(0);
                record.set('parentid',grid_iu_urok_comments.parentid);
                edit.getComponent(0).setActiveRecord(record,grid_iu_urok_comments.instanceid,grid_iu_urok_comments.parentid);
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
            grid_iu_urok_comments.store.load({params:{parentid: grid_iu_urok_comments.parentid}});
    };
    function ChildOnEditClick(){
        var selection = grid_iu_urok_comments.getView().getSelectionModel().getSelection()[0];
        if (selection) {
   	     if(CheckOperation('iu_urok.edit')!=0){
            var edit = Ext.create('EditWindow_iu_urok_comments');
            edit.getComponent(0).setActiveRecord(selection,grid_iu_urok_comments.instanceid,grid_iu_urok_comments.parentid);
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
   grid_iu_urok_comments=
    new Ext.grid.Panel({
        itemId:  'grd_iu_urok_comments',
        maxHeight: 350,
        minHeight: 250,
        iconCls:  'icon-grid',
        frame: true,
        parentid: '',
        title: 'Комментарии к файлу',
        scroll:'both',
        store: {
        model:'model_iu_urok_comments',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   'index.php/c_iu_urok_comments/getRows',
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
        features: [groupingFeature_iu_urok_comments],
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
{text: "Маркет времени", width: 200, dataIndex: 'timemarker', sortable: true}
            ,
{text: "Дата", width:80, dataIndex: 'thedate', sortable: true, xtype: 'datecolumn',   format:'Y-m-d'}
            ,
{text: "Добавил", width: 200, dataIndex: 'theauthor_grid', sortable: true}
            ,
{text: "Информация", width: 200, dataIndex: 'info', sortable: true}
        ],
    listeners: {
        itemdblclick: function() { 
    	    ChildOnEditClick();
        },
          itemclick: function(view , record){
         grid_iu_urok_comments.down('#delete').setDisabled(false);
          grid_iu_urok_comments.down('#edit').setDisabled(false);
    },
    select: function( selmodel, record,  index,  eOpts ){
        grid_iu_urok_comments.down('#delete').setDisabled(false);
        grid_iu_urok_comments.down('#edit').setDisabled(false);
    }, 
    selectionchange: function(selModel, selections){
    	 grid_iu_urok_comments.down('#delete').setDisabled(selections.length === 0);
    	 grid_iu_urok_comments.down('#edit').setDisabled(selections.length === 0);
    }
    }
    }
    );
var groupingFeature_iu_urok_docs = Ext.create('Ext.grid.feature.Grouping',{
groupByText:  'Группировать по этому полю',
showGroupsText:  'Показать группировку'
});
var p1;
    function onDeleteConfirm(selection){
      if (selection) {
        Ext.Ajax.request({
            url:    'index.php/c_iu_urok_docs/deleteRow',
            method:  'POST',
    		params: { 
    				iu_urok_docsid: selection.get('iu_urok_docsid')
    				}
    	});
    	p1.store.remove(selection);
      }
    };
    function onDeleteClick(){
      var selection = p1.getView().getSelectionModel().getSelection()[0];
      if (selection) {
   	  if(CheckOperation('iu_urok.edit')!=0){
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
   	if(CheckOperation('iu_urok.edit')!=0){
                var edit = Ext.create('EditWindow_iu_urok_docs');
                p1.store.insert(0, new model_iu_urok_docs());
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
   	    if(CheckOperation('iu_urok.edit')!=0){
            var edit = Ext.create('EditWindow_iu_urok_docs');
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
            flex: 1,
        layout:'fit',
        iconCls:  'icon-grid',
        frame: true,
        instanceid: '',
        scroll:'both',
        features: [groupingFeature_iu_urok_docs],
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
{text: "Тип документа", width:200, dataIndex: 'doctype_grid', sortable: true}
            ,
{text: "Кем добавлен", width:200, dataIndex: 'addby_grid', sortable: true}
            ,
{text: "Когда добавлен", width:80, dataIndex: 'adddate', sortable: true}
            ,
{text: "№ версии", width:60, dataIndex: 'version', sortable: true}
            ,
{text: "Активная версия", width:60, dataIndex: 'activeversion_grid', sortable: true}
            ,
{ text     : 'Файл', xtype: 'templatecolumn',  align:'right',width    : 90,	sortable : false,
 tpl: '<a href=\'/output_file.php?ID={fileref}&ext={filer_ext}\' target=\'_blank\'>Get file</a>'},

{text: "Описание", width: 200, dataIndex: 'info', sortable: true }
        ]
,
	bbar:grid_iu_urok_comments, 
    listeners: {
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
           grid_iu_urok_comments.instanceid=p1.instanceid;
           grid_iu_urok_comments.parentid=record.get('iu_urok_docsid');
           grid_iu_urok_comments.store.load({params:{ parentid:record.get('iu_urok_docsid')} })
        },
        selectionchange: function(selModel, selections){
        p1.down('#delete').setDisabled(selections.length === 0);
        p1.down('#edit').setDisabled(selections.length === 0);
        var selection = selections[0];
        if (selection) {
           p1.down('#grd_iu_urok_comments').instanceid=p1.instanceid;
           p1.down('#grd_iu_urok_comments').parentid=selection.get('iu_urok_docsid');
           p1.down('#grd_iu_urok_comments').store.load({params:{ parentid:selection.get('iu_urok_docsid')} })
        }
       }
    }
    }
    );
return p1;
};
function DefineForms_iu_urok_docs_(){


Ext.define('Form_iu_urok_docs', {
extend:  'Ext.form.Panel',
alias: 'widget.f_iu_urok_docs',
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
        xtype:'fieldset', 
        id:'iu_urok_docs-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 0, 

editable: false,
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('doctype', records[0].get('id'));}  },
xtype:  'combobox',
store: cmbstore_iud_doctype,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'doctype_grid',
itemId:   'doctype_grid',
fieldLabel:  'Тип документа',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 46, 

trigger1Cls:        'x-form-clear-trigger', 
trigger2Cls:        'x-form-select-trigger', 
hideTrigger1:false, 
hideTrigger2:false, 
onTrigger1Click : function(){
		this.collapse();
		this.clearValue();
		this.up('form' ).activeRecord.set('addby',null );
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
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('addby', records[0].get('id'));}  },
xtype:  'combobox',
store: cmbstore_iu_u_def,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'addby_grid',
itemId:   'addby_grid',
fieldLabel:  'Кем добавлен',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 92, 

xtype:  'datefield',
format:'d/m/Y H:i:s',
submitFormat:'Y-m-d H:i:s',
value:  '',
name:   'adddate',
itemId:   'adddate',
fieldLabel:  'Когда добавлен',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 138, 

xtype:  'numberfield',
value:  '0',
name:   'version',
itemId:   'version',
fieldLabel:  '№ версии',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 184, 

xtype:          'checkboxfield',
uncheckedValue: 0,
inputValue:    -1,
name:   'activeversion',
itemId:   'activeversion',
fieldLabel:  'Активная версия',
labelSeparator:'',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 230, 
xtype:  'filefield',
name:   'fileref_fu',
itemId:   'fileref_fu',
fieldLabel:  'Ссылка на файл',
allowBlank:true
       ,labelWidth: 120
},{xtype:  'hiddenfield',
name:   'fileref',
itemId:   'fileref',
}
,{
xtype:  'hiddenfield',
name:   'fileref_ext',
itemId:   'fileref_ext',
},
{
        minWidth: 480,
        xtype: 'textarea', 
        x: 5, 
        y: 276, 
        height: 80, 

value:  '',
name:   'info',
itemId:   'info',
fieldLabel:  'Описание',
allowBlank:true
       ,labelWidth: 120
}
       ], width: 510,
       height: 406 
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
		form.updateRecord(active);
            // combobox patch
		var form_values = form.getValues(); var field_name = '';  for(field_name in form_values){active.set(field_name, form_values[field_name]);}
		StatusDB('Сохранение данных');
        if (form.isValid()) {
			 form.submit(
					{
							url: 'index.php/c_iu_urok_docs/setRow',
							method:  'POST',
						    params: { 
								instanceid: this.instanceid
								,iu_urok_docsid: active.get('iu_urok_docsid')
								,doctype: active.get('doctype') 
								,addby: active.get('addby') 
							},
							success: function() {
								 StatusReady('Изменения сохранены');
								 form.owner.ownerCt.close();
							},
							failure: function(){
								StatusReady('Ошибка сохранения данных');
							}
                    }
				);
				/*
            form.updateRecord(active);
            // combobox patch
            var form_values = form.getValues(); var field_name = '';  for(field_name in form_values){active.set(field_name, form_values[field_name]);}
        	StatusDB('Сохранение данных');
            Ext.Ajax.request({
                url: 'index.php/c_iu_urok_docs/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid
                    ,iu_urok_docsid: active.get('iu_urok_docsid')
                    ,doctype: active.get('doctype') 
                    ,addby: active.get('addby') 
                    ,adddate: active.get('adddate').toLocaleFormat('%Y-%m-%d %H:%M:%S') 
                    ,version: active.get('version') 
                    ,activeversion: active.get('activeversion') 
                    ,fileref: active.get('fileref') 
                    ,info: active.get('info') 
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
                    if(active.get('iu_urok_docsid')==''){
               			active.set('iu_urok_docsid',res.data['iu_urok_docsid']);
                    }
        		  
        		    StatusReady('Изменения сохранены');
                form.owner.ownerCt.close();
                }
              }
            }); */
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
        if(this.activeRecord.get('iu_urok_docsid')==''){
                this.activeRecord.store.reload();
        }
        this.setActiveRecord(null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_iu_urok_docs', {
    extend:  'Ext.window.Window',
    maxHeight: 511,
    maxWidth: 540,
    minHeight:466,
    minWidth: 540,
    width: 540,
    constrainHeader :true,
    layout:  'absolute',
    autoShow: true,
    modal: true,
    closable: false,
    closeAction: 'destroy',
    iconCls:  'icon-application_form',
    title:  'Архив',
    items:[{
        xtype:  'f_iu_urok_docs'
	}]
	});

Ext.define('Form_iu_urok_comments', {
extend:  'Ext.form.Panel',
alias: 'widget.f_iu_urok_comments',
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
        xtype:'fieldset', 
        id:'iu_urok_comments-0',
        layout:'absolute', 
        border:false, 
        items: [
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 0, 

xtype:  'textfield',
value:  '',
name:   'timemarker',
itemId:   'timemarker',
fieldLabel:  'Маркет времени',
allowBlank:true
       ,labelWidth: 120
}
,
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 46, 

xtype:  'datefield',
format:'d/m/Y H:i:s',
submitFormat:'Y-m-d H:i:s',
value:  '',
name:   'thedate',
itemId:   'thedate',
fieldLabel:  'Дата',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 470,
        width: 470,
        maxWidth: 470,
        x: 5, 
        y: 92, 

editable: false,
listeners:{  select: function ( combo, records, eOpts ) {combo.up('form' ).activeRecord.set('theauthor', records[0].get('id'));}  },
xtype:  'combobox',
store: cmbstore_iu_u_def,
valueField:     'brief',
displayField:   'brief',
typeAhead: true,
emptyText:      '',
name:   'theauthor_grid',
itemId:   'theauthor_grid',
fieldLabel:  'Добавил',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
,
{
        minWidth: 480,
        x: 5, 
        y: 138, 

xtype:  'textfield',
value:  '',
name:   'info',
itemId:   'info',
fieldLabel:  'Информация',
labelClsExtra:'x-item-mandatory',
allowBlank:false
       ,labelWidth: 120
}
       ], width: 510,
       height: 224 
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
            var form_values = form.getValues(); var field_name = '';  for(field_name in form_values){active.set(field_name, form_values[field_name]);}
        	StatusDB('Сохранение данных');
            Ext.Ajax.request({
                url: 'index.php/c_iu_urok_comments/setRow',
                method:  'POST',
                params: { 
                    instanceid: this.instanceid,
                    parentid: this.parentid
                    ,iu_urok_commentsid: active.get('iu_urok_commentsid')
                    ,timemarker: active.get('timemarker') 
                    ,thedate: active.get('thedate').toLocaleFormat('%Y-%m-%d %H:%M:%S') 
                    ,theauthor: active.get('theauthor') 
                    ,info: active.get('info') 
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
                    if(active.get('iu_urok_commentsid')==''){
               			active.set('iu_urok_commentsid',res.data['iu_urok_commentsid']);
                    }
        		   /* Ext.MessageBox.show({
                        title:  'Подтверждение',
                        msg:    'Изменения сохранены',
                        buttons: Ext.MessageBox.OK,
                        icon:   Ext.MessageBox.INFO
        		    }); */
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
        if(this.activeRecord.get('iu_urok_commentsid')==''){
                this.activeRecord.store.reload();
        }
        this.setActiveRecord(null,null);
        this.ownerCt.close();
    }
}); // 'Ext.Define

Ext.define('EditWindow_iu_urok_comments', {
    extend:  'Ext.window.Window',
    maxHeight: 329,
    maxWidth: 540,
    minHeight:284,
    minWidth: 540,
    width: 540,
    constrainHeader :true,
    layout:  'absolute',
    autoShow: true,
    modal: true,
    closable: false,
    closeAction: 'destroy',
    iconCls:  'icon-application_form',
    title:  'Комментарии к файлу',
    items:[{
        xtype:  'f_iu_urok_comments'
	}]
	});
}