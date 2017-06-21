
 Ext.define('model_v_autobp3dic_gen',{
            extend:'Ext.data.Model',
        fields: [
            {name: 'instanceid',type: 'string'}
            ,{name: 'id',type: 'string'}
            ,{name:'bp3dic_gen_targettype', type: 'string'}
            ,{name:'bp3dic_gen_thedevelopmentenv', type: 'string'}
            ,{name:'bp3dic_gen_name', type: 'string'}
            ,{name:'bp3dic_gen_generatorprogid', type: 'string'}
            ,{name:'bp3dic_gen_generatorstyle', type: 'string'}
            ,{name:'bp3dic_gen_queuename', type: 'string'}
        ]
    });

    var store_v_autobp3dic_gen = Ext.create('Ext.data.Store', {
        model:'model_v_autobp3dic_gen',
        autoLoad: false,
        autoSync: false,
        remoteSort: true,
        remoteFilter:true,
        pageSize : 30,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_v_autobp3dic_gen/getRows',
            reader: {
                type:   'json'
                ,root:  'rows'
                ,totalProperty: 'total'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            },
            listeners: {
                exception: function(proxy, response, operation){
                    Ext.MessageBox.show({
                        title: 'REMOTE EXCEPTION',
                        msg:    operation.getError(),
                        icon : Ext.MessageBox.ERROR,
                        buttons : Ext.Msg.OK
                    });
                }
            }
        }
    });