
Ext.require([
'Ext.form.*'
]);
  bp3card_= null;
function bp3card_Panel_(objectID, RootPanel, selection){ 


    var store_bp3card_def = Ext.create('Ext.data.Store', {
        model:'model_bp3card_def',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3card_def/getRows',
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

    var store_bp3card_part = Ext.create('Ext.data.Store', {
        model:'model_bp3card_part',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3card_part/getRows',
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

    var store_bp3card_fld = Ext.create('Ext.data.Store', {
        model:'model_bp3card_fld',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3card_fld/getRows',
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
          DefineForms_bp3card_def_();
          DefineForms_bp3card_part_();
          DefineForms_bp3card_fld_();
     var   int_bp3card_def_     =      DefineInterface_bp3card_def_('int_bp3card_def',store_bp3card_def, selection);
     var   int_bp3card_part_     =      DefineInterface_bp3card_part_('int_bp3card_part',store_bp3card_part);
     var   int_bp3card_fld_     =      DefineInterface_bp3card_fld_('int_bp3card_fld',store_bp3card_fld);
     bp3card_= Ext.create('Ext.form.Panel', {
      id: 'bp3card',
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
        	    int_bp3card_def_.doSave( me.onCommit);
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return int_bp3card_def_.canClose();
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3card',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Карточка документа',
            layout:'fit',
            itemId:'tab_bp3card_def',
            items:[ // panel on tab 
int_bp3card_def_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Раздел',
            layout:'fit',
            itemId:'tab_bp3card_part',
            items:[ // panel on tab 
int_bp3card_part_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Поля карточки',
            layout:'fit',
            itemId:'tab_bp3card_fld',
            items:[ // panel on tab 
int_bp3card_fld_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3card_.closable= true;
       bp3card_.title= 'Карточка';
       bp3card_.frame= true;
    }else{
       bp3card_.closable= false;
       bp3card_.title= '';
       bp3card_.frame= false;
    }
   store_bp3card_def.on('load', function() {
        if(store_bp3card_def.count()==0){
            store_bp3card_def.insert(0, new model_bp3card_def());
        }
        record= store_bp3card_def.getAt(0);
        record['instanceid']=objectID;
   int_bp3card_def_.setActiveRecord(record,objectID);	
   });
       store_bp3card_def.load( {params:{ instanceid:objectID} }  );
   int_bp3card_part_.instanceid=objectID;	
       store_bp3card_part.load(  {params:{ instanceid:objectID} } );
   int_bp3card_fld_.instanceid=objectID;	
       store_bp3card_fld.load(  {params:{ instanceid:objectID} } );
    return bp3card_;
}