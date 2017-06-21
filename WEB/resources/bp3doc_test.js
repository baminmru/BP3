
Ext.require([
'Ext.form.*'
]);
  bp3doc_test= null;
function bp3doc_Panel_test(objectID, RootPanel, selection){ 


    var store_bp3doc_def = Ext.create('Ext.data.Store', {
        model:'model_bp3doc_def',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3doc_def/getRows',
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

    var treestore_bp3doc_store = Ext.create('Ext.data.TreeStore', {
        model:'model_bp3doc_store',
        autoLoad: false,
        autoSync: false,
        //folderSort: true,
        nodeParam:  'treeid',
        defaultRootId:  '{00000000-0000-0000-0000-000000000000}',
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3doc_store/getRows',
            reader: {
                type:   'json'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });

    var store_bp3doc_field = Ext.create('Ext.data.Store', {
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
            }
        }
    });

    var store_bp3doc_uk = Ext.create('Ext.data.Store', {
        model:'model_bp3doc_uk',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3doc_uk/getRows',
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

    var store_bp3doc_ukfld = Ext.create('Ext.data.Store', {
        model:'model_bp3doc_ukfld',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3doc_ukfld/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            }
        }
    });
          DefineForms_bp3doc_def_test();
          DefineForms_bp3doc_store_test();
          DefineForms_bp3doc_uk_test();
     var   int_bp3doc_def_test     =      DefineInterface_bp3doc_def_test('int_bp3doc_def',store_bp3doc_def, selection);
  var   int_bp3doc_store_test     = DefineInterface_bp3doc_store_test('int_bp3doc_store', treestore_bp3doc_store);
     var   int_bp3doc_uk_test     =      DefineInterface_bp3doc_uk_test('int_bp3doc_uk',store_bp3doc_uk);
     bp3doc_test= Ext.create('Ext.form.Panel', {
      id: 'bp3doc',
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
        	    int_bp3doc_def_test.doSave( me.onCommit);
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return int_bp3doc_def_test.canClose();
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3doc',
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
            itemId:'tab_bp3doc_def',
            items:[ // panel on tab 
int_bp3doc_def_test
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Раздел',
            layout:'fit',
            itemId:'tab_bp3doc_store',
            items:[ // panel on tab 
int_bp3doc_store_test
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Ограничение уникальности',
            layout:'fit',
            itemId:'tab_bp3doc_uk',
            items:[ // panel on tab 
int_bp3doc_uk_test
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3doc_test.closable= true;
       bp3doc_test.title= 'Документ';
       bp3doc_test.frame= true;
    }else{
       bp3doc_test.closable= false;
       bp3doc_test.title= '';
       bp3doc_test.frame= false;
    }
   store_bp3doc_def.on('load', function() {
        if(store_bp3doc_def.count()==0){
            store_bp3doc_def.insert(0, new model_bp3doc_def());
        }
        record= store_bp3doc_def.getAt(0);
        record['instanceid']=objectID;
   int_bp3doc_def_test.setActiveRecord(record,objectID);	
   });
       store_bp3doc_def.load( {params:{ instanceid:objectID} }  );
   int_bp3doc_store_test.instanceid=objectID;	
   int_bp3doc_uk_test.instanceid=objectID;	
       store_bp3doc_uk.load(  {params:{ instanceid:objectID} } );
    return bp3doc_test;
}