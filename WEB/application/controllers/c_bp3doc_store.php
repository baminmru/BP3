<?php
	 class C_bp3doc_store extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3doc_store.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3doc_store.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3doc_storeid' =>  $this->input->get_post('bp3doc_storeid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'treeid' =>  $this->input->get_post('treeid', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'parttype' =>   $this->input->get_post('parttype', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'the_comment' =>   $this->input->get_post('the_comment', TRUE)
                ,'isdocinstance' =>   $this->input->get_post('isdocinstance', TRUE)
                ,'usearchiving' =>   $this->input->get_post('usearchiving', TRUE)
                ,'nolog' =>   $this->input->get_post('nolog', TRUE)
                ,'shablonbrief' =>   $this->input->get_post('shablonbrief', TRUE)
                ,'usechangelog' =>   $this->input->get_post('usechangelog', TRUE)
                ,'rulebrief' =>   $this->input->get_post('rulebrief', TRUE)
            );
            $bp3doc_store = $this->m_bp3doc_store->setRow($data);
            print json_encode($bp3doc_store);
    }
    function newRow() {
            log_message('debug', 'bp3doc_store.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3doc_storeid' =>  $this->input->get_post('bp3doc_storeid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'treeid' =>  $this->input->get_post('treeid', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'parttype' =>   $this->input->get_post('parttype', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'the_comment' =>   $this->input->get_post('the_comment', TRUE)
                ,'isdocinstance' =>   $this->input->get_post('isdocinstance', TRUE)
                ,'usearchiving' =>   $this->input->get_post('usearchiving', TRUE)
                ,'nolog' =>   $this->input->get_post('nolog', TRUE)
                ,'shablonbrief' =>   $this->input->get_post('shablonbrief', TRUE)
                ,'usechangelog' =>   $this->input->get_post('usechangelog', TRUE)
                ,'rulebrief' =>   $this->input->get_post('rulebrief', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $treeid=$this->input->post('treeid', FALSE);
            $bp3doc_store= $this->m_bp3doc_store->newRow($instanceid,$treeid,$data);
            $return = $bp3doc_store;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3doc_store.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
            $treeid  =  $this->input->get_post('treeid', TRUE);
        if (isset($empId)) {
            $bp3doc_store = $this->m_bp3doc_store->getRow($empId,$treeid);
            $return = array(
                'success' => true,
                'data'    => $bp3doc_store
            );
            print json_encode($return);
        }
    }
    function getRows() {
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
           	$treeid=$this->input->get('treeid', FALSE);
                     if(isset($instanceid)){
                         if($instanceid!=''){
           			if(isset($treeid)){
           				log_message('debug', 'bp3doc_store.getRows getRowsByInstanceTree');
           				$bp3doc_store= $this->m_bp3doc_store->getRowsByInstanceTree($instanceid,$treeid,$sort);
           			}else{
           				log_message('debug', 'bp3doc_store.getRows getRowsByInstance');
           				$bp3doc_store= $this->m_bp3doc_store->getRowsByInstance($instanceid,$sort);
           			}
                         }else{
                             if(isset($treeid)){
           				log_message('debug', 'bp3doc_store.getRows getRowsByTree');
           				$bp3doc_store= $this->m_bp3doc_store->getRowsByTree($treeid,$sort);
           			}else{
           				log_message('debug', 'bp3doc_store.getRows getRows');
           				$bp3doc_store= $this->m_bp3doc_store->getRows($sort);
           			}
                         }
                     }else{
           			if(isset($treeid)){
           				log_message('debug', 'bp3doc_store.getRows getRowsByTree');
           				$bp3doc_store= $this->m_bp3doc_store->getRowsByTree($treeid,$sort);
           			}else{
           				log_message('debug', 'bp3doc_store.getRows getRows');
           				$bp3doc_store= $this->m_bp3doc_store->getRows($sort);
           			}
                     }
           print json_encode($bp3doc_store);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3doc_store.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3doc_store= $this->m_bp3doc_store->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3doc_store
            );
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'Need instanceid to query.'
            );
        }
        print json_encode($return);
    }
    function getRowsByParent() {
        log_message('debug', 'bp3doc_store.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3doc_store= $this->m_bp3doc_store->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3doc_store
            );
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'Need parent parentid to query.'
            );
        }
        print json_encode($return);
    }
    function deleteRow() {
        log_message('debug', 'bp3doc_store.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3doc_storeid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3doc_store->deleteRow($tempId);
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'No  ID to delete'
            );
        }
        print json_encode($return);
    }
    private function _loadModels () {
        $this->load->model('M_bp3doc_store', 'm_bp3doc_store');
    }
}
?>