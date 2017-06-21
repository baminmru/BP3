
Ext.require([
'Ext.form.*'
]);
  bp3list_= null;
function bp3list_Panel_(objectID, RootPanel, selection){ 


    var store_bp3list_def = Ext.create('Ext.data.Store', {
        model:'model_bp3list_def',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3list_def/getRows',
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

    var store_bp3list_col = Ext.create('Ext.data.Store', {
        model:'model_bp3list_col',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3list_col/getRows',
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

    var store_bp3list_filter = Ext.create('Ext.data.Store', {
        model:'model_bp3list_filter',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3list_filter/getRows',
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

    var store_bp3list_filterfield = Ext.create('Ext.data.Store', {
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
            }
        }
    });
          DefineForms_bp3list_def_();
          DefineForms_bp3list_col_();
          DefineForms_bp3list_filter_();
     var   int_bp3list_def_     =      DefineInterface_bp3list_def_('int_bp3list_def',store_bp3list_def, selection);
     var   int_bp3list_col_     =      DefineInterface_bp3list_col_('int_bp3list_col',store_bp3list_col);
     var   int_bp3list_filter_     =      DefineInterface_bp3list_filter_('int_bp3list_filter',store_bp3list_filter);
     bp3list_= Ext.create('Ext.form.Panel', {
      id: 'bp3list',
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
        	    int_bp3list_def_.doSave( me.onCommit);
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return int_bp3list_def_.canClose();
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3list',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Журнал',
            layout:'fit',
            itemId:'tab_bp3list_def',
            items:[ // panel on tab 
int_bp3list_def_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Колонки журнала',
            layout:'fit',
            itemId:'tab_bp3list_col',
            items:[ // panel on tab 
int_bp3list_col_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Группа полей фильтра',
            layout:'fit',
            itemId:'tab_bp3list_filter',
            items:[ // panel on tab 
int_bp3list_filter_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3list_.closable= true;
       bp3list_.title= 'Журнал документов';
       bp3list_.frame= true;
    }else{
       bp3list_.closable= false;
       bp3list_.title= '';
       bp3list_.frame= false;
    }
   store_bp3list_def.on('load', function() {
        if(store_bp3list_def.count()==0){
            store_bp3list_def.insert(0, new model_bp3list_def());
        }
        record= store_bp3list_def.getAt(0);
        record['instanceid']=objectID;
   int_bp3list_def_.setActiveRecord(record,objectID);	
   });
       store_bp3list_def.load( {params:{ instanceid:objectID} }  );
   int_bp3list_col_.instanceid=objectID;	
       store_bp3list_col.load(  {params:{ instanceid:objectID} } );
   int_bp3list_filter_.instanceid=objectID;	
       store_bp3list_filter.load(  {params:{ instanceid:objectID} } );
    return bp3list_;
}