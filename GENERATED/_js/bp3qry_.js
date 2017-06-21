
Ext.require([
'Ext.form.*'
]);
  bp3qry_= null;
function bp3qry_Panel_(objectID, RootPanel, selection){ 


    var store_bp3qry_def = Ext.create('Ext.data.Store', {
        model:'model_bp3qry_def',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3qry_def/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });

    var store_bp3qry_column = Ext.create('Ext.data.Store', {
        model:'model_bp3qry_column',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3qry_column/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });

    var store_bp3qry_link = Ext.create('Ext.data.Store', {
        model:'model_bp3qry_link',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3qry_link/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });
          DefineForms_bp3qry_def_();
          DefineForms_bp3qry_column_();
          DefineForms_bp3qry_link_();
     var   int_bp3qry_def_     =      DefineInterface_bp3qry_def_('int_bp3qry_def',store_bp3qry_def, selection);
     var   int_bp3qry_column_     =      DefineInterface_bp3qry_column_('int_bp3qry_column',store_bp3qry_column);
     var   int_bp3qry_link_     =      DefineInterface_bp3qry_link_('int_bp3qry_link',store_bp3qry_link);
     bp3qry_= Ext.create('Ext.form.Panel', {
      id: 'bp3qry',
      layout:'fit',
      fieldDefaults: {
          labelAlign:             'top',
          msgTarget:              'side'
        },
        defaults: {
        anchor:'100%'
        },

        instanceid:objectID,
                onCommit: function(){
        		},
        		onButtonOk: function()
        		{
        			var me = this;
        	    int_bp3qry_def_.doSave( me.onCommit);
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return int_bp3qry_def_.canClose();
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3qry',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Представление',
            layout:'fit',
            itemId:'tab_bp3qry_def',
            items:[ // panel on tab 
int_bp3qry_def_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Колонка',
            layout:'fit',
            itemId:'tab_bp3qry_column',
            items:[ // panel on tab 
int_bp3qry_column_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Связанные представления',
            layout:'fit',
            itemId:'tab_bp3qry_link',
            items:[ // panel on tab 
int_bp3qry_link_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3qry_.closable= true;
       bp3qry_.title= 'Запрос';
       bp3qry_.frame= true;
    }else{
       bp3qry_.closable= false;
       bp3qry_.title= '';
       bp3qry_.frame= false;
    }
   store_bp3qry_def.on('load', function() {
        if(store_bp3qry_def.count()==0){
            store_bp3qry_def.insert(0, new model_bp3qry_def());
        }
        record= store_bp3qry_def.getAt(0);
        record['instanceid']=objectID;
   int_bp3qry_def_.setActiveRecord(record,objectID);	
   });
       store_bp3qry_def.load( {params:{ instanceid:objectID} }  );
   int_bp3qry_column_.instanceid=objectID;	
       store_bp3qry_column.load(  {params:{ instanceid:objectID} } );
   int_bp3qry_link_.instanceid=objectID;	
       store_bp3qry_link.load(  {params:{ instanceid:objectID} } );
    return bp3qry_;
}