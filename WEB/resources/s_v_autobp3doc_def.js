﻿
 Ext.define('model_v_autobp3doc_def',{
            extend:'Ext.data.Model',
        fields: [
            {name: 'instanceid',type: 'string'}
            ,{name: 'id',type: 'string'}
            ,{name:'bp3doc_def_commitfullobject', type: 'string'}
            ,{name:'bp3doc_def_thecaption', type: 'string'}
            ,{name:'bp3doc_def_name', type: 'string'}
            ,{name:'bp3doc_def_issingleinstance', type: 'string'}
            ,{name:'bp3doc_def_useownership', type: 'string'}
            ,{name:'bp3doc_def_thecomment', type: 'string'}
            ,{name:'bp3doc_def_usearchiving', type: 'string'}
        ]
    });

    var store_v_autobp3doc_def = Ext.create('Ext.data.Store', {
        model:'model_v_autobp3doc_def',
        autoLoad: false,
        autoSync: false,
        remoteSort: true,
        remoteFilter:true,
        pageSize : 30,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_v_autobp3doc_def/getRows',
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