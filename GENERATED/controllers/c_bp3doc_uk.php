<?php
	 class C_bp3doc_uk extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3doc_uk.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3doc_uk.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3doc_ukid' =>  $this->input->get_post('bp3doc_ukid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'ukstore' =>   $this->input->get_post('ukstore', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'perparent' =>   $this->input->get_post('perparent', TRUE)
            );
            $bp3doc_uk = $this->m_bp3doc_uk->setRow($data);
            print json_encode($bp3doc_uk);
    }
    function newRow() {
            log_message('debug', 'bp3doc_uk.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3doc_ukid' =>  $this->input->get_post('bp3doc_ukid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'ukstore' =>   $this->input->get_post('ukstore', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'perparent' =>   $this->input->get_post('perparent', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3doc_uk= $this->m_bp3doc_uk->newRow($instanceid,$data);
            $return = $bp3doc_uk;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3doc_uk.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3doc_uk = $this->m_bp3doc_uk->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3doc_uk
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
            	$sort[] = array('property'=>'ukstore', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3doc_uk= $this->m_bp3doc_uk->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3doc_uk= $this->m_bp3doc_uk->getRows($sort);
                }
            }else{
              $bp3doc_uk= $this->m_bp3doc_uk->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3doc_uk
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3doc_uk.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'ukstore', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3doc_uk= $this->m_bp3doc_uk->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3doc_uk
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
        log_message('debug', 'bp3doc_uk.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'ukstore', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3doc_uk= $this->m_bp3doc_uk->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3doc_uk
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
        log_message('debug', 'bp3doc_uk.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3doc_ukid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3doc_uk->deleteRow($tempId);
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
        $this->load->model('M_bp3doc_uk', 'm_bp3doc_uk');
    }
}
?>