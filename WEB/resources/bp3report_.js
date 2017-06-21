
Ext.require([
'Ext.form.*'
]);
  bp3report_= null;
function bp3report_Panel_(objectID, RootPanel, selection){ 


    var store_bp3report_def = Ext.create('Ext.data.Store', {
        model:'model_bp3report_def',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3report_def/getRows',
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

    var store_bp3report_filter = Ext.create('Ext.data.Store', {
        model:'model_bp3report_filter',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3report_filter/getRows',
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

    var store_bp3report_filterfiel = Ext.create('Ext.data.Store', {
        model:'model_bp3report_filterfiel',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3report_filterfiel/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            }
        }
    });
          DefineForms_bp3report_def_();
          DefineForms_bp3report_filter_();
     var   int_bp3report_def_     =      DefineInterface_bp3report_def_('int_bp3report_def',store_bp3report_def, selection);
     var   int_bp3report_filter_     =      DefineInterface_bp3report_filter_('int_bp3report_filter',store_bp3report_filter);
     bp3report_= Ext.create('Ext.form.Panel', {
      id: 'bp3report',
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
        	    int_bp3report_def_.doSave( me.onCommit);
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return int_bp3report_def_.canClose();
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3report',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Описание',
            layout:'fit',
            itemId:'tab_bp3report_def',
            items:[ // panel on tab 
int_bp3report_def_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Группа полей фильтра',
            layout:'fit',
            itemId:'tab_bp3report_filter',
            items:[ // panel on tab 
int_bp3report_filter_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3report_.closable= true;
       bp3report_.title= 'Отчет';
       bp3report_.frame= true;
    }else{
       bp3report_.closable= false;
       bp3report_.title= '';
       bp3report_.frame= false;
    }
   store_bp3report_def.on('load', function() {
        if(store_bp3report_def.count()==0){
            store_bp3report_def.insert(0, new model_bp3report_def());
        }
        record= store_bp3report_def.getAt(0);
        record['instanceid']=objectID;
   int_bp3report_def_.setActiveRecord(record,objectID);	
   });
       store_bp3report_def.load( {params:{ instanceid:objectID} }  );
   int_bp3report_filter_.instanceid=objectID;	
       store_bp3report_filter.load(  {params:{ instanceid:objectID} } );
    return bp3report_;
}