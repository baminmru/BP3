﻿
 Ext.define('model_v_autobp3list_def',{
            extend:'Ext.data.Model',
        fields: [
            {name: 'instanceid',type: 'string'}
            ,{name: 'id',type: 'string'}
            ,{name:'bp3list_def_onrun', type: 'string'}
            ,{name:'bp3list_def_name', type: 'string'}
            ,{name:'bp3list_def_editcard', type: 'string'}
            ,{name:'bp3list_def_thecomment', type: 'string'}
            ,{name:'bp3list_def_newcard', type: 'string'}
            ,{name:'bp3list_def_sourceview', type: 'string'}
        ]
    });

    var store_v_autobp3list_def = Ext.create('Ext.data.Store', {
        model:'model_v_autobp3list_def',
        autoLoad: false,
        autoSync: false,
        remoteSort: true,
        remoteFilter:true,
        pageSize : 30,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_v_autobp3list_def/getRows',
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