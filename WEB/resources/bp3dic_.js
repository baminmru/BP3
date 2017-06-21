
Ext.require([
'Ext.form.*'
]);
  bp3dic_= null;
function bp3dic_Panel_(objectID, RootPanel, selection){ 


    var store_bp3dic_gen = Ext.create('Ext.data.Store', {
        model:'model_bp3dic_gen',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3dic_gen/getRows',
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
          DefineForms_bp3dic_gen_();
     var   int_bp3dic_gen_     =      DefineInterface_bp3dic_gen_('int_bp3dic_gen',store_bp3dic_gen);
     bp3dic_= Ext.create('Ext.form.Panel', {
      id: 'bp3dic',
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
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return true;
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3dic',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Генераторы',
            layout:'fit',
            itemId:'tab_bp3dic_gen',
            items:[ // panel on tab 
int_bp3dic_gen_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3dic_.closable= true;
       bp3dic_.title= 'Справочники моделлера';
       bp3dic_.frame= true;
    }else{
       bp3dic_.closable= false;
       bp3dic_.title= '';
       bp3dic_.frame= false;
    }
   int_bp3dic_gen_.instanceid=objectID;	
       store_bp3dic_gen.load(  {params:{ instanceid:objectID} } );
    return bp3dic_;
}