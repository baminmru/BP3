<?php
	 class C_bp3doc_ukfld extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3doc_ukfld.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3doc_ukfld.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3doc_ukfldid' =>  $this->input->get_post('bp3doc_ukfldid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'thefield' =>   $this->input->get_post('thefield', TRUE)
            );
            $bp3doc_ukfld = $this->m_bp3doc_ukfld->setRow($data);
            print json_encode($bp3doc_ukfld);
    }
    function newRow() {
            log_message('debug', 'bp3doc_ukfld.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3doc_ukfldid' =>  $this->input->get_post('bp3doc_ukfldid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'thefield' =>   $this->input->get_post('thefield', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $bp3doc_ukfld= $this->m_bp3doc_ukfld->newRow($instanceid,$parentid,$data);
            $return = $bp3doc_ukfld;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3doc_ukfld.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3doc_ukfld = $this->m_bp3doc_ukfld->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3doc_ukfld
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
            	$sort[] = array('property'=>'thefield', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $parentid=$this->input->get('parentid', FALSE);
            if(isset($parentid)){
                if($parentid!=''){
                    $bp3doc_ukfld= $this->m_bp3doc_ukfld->getRowsByParent($parentid,$sort);
                }else{
                    $bp3doc_ukfld= $this->m_bp3doc_ukfld->getRows($sort);
                }
            }else{
              $bp3doc_ukfld= $this->m_bp3doc_ukfld->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3doc_ukfld
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3doc_ukfld.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'thefield', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3doc_ukfld= $this->m_bp3doc_ukfld->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3doc_ukfld
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
        log_message('debug', 'bp3doc_ukfld.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'thefield', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3doc_ukfld= $this->m_bp3doc_ukfld->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3doc_ukfld
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
        log_message('debug', 'bp3doc_ukfld.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3doc_ukfldid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3doc_ukfld->deleteRow($tempId);
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
        $this->load->model('M_bp3doc_ukfld', 'm_bp3doc_ukfld');
    }
}
?>