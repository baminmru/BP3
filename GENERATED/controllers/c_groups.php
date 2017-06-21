<?php
	 class C_groups extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'Groups.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'Groups.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'groupsid' =>  $this->input->get_post('groupsid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'adgroup' =>   $this->input->get_post('adgroup', TRUE)
            );
            $groups = $this->m_groups->setRow($data);
            print json_encode($groups);
    }
    function newRow() {
            log_message('debug', 'Groups.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'groupsid' =>  $this->input->get_post('groupsid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'adgroup' =>   $this->input->get_post('adgroup', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $groups= $this->m_groups->newRow($instanceid,$data);
            $return = $groups;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'Groups.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $groups = $this->m_groups->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $groups
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
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $groups= $this->m_groups->getRowsByInstance($instanceid,$sort);
                }else{
                    $groups= $this->m_groups->getRows($sort);
                }
            }else{
              $groups= $this->m_groups->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $groups
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'Groups.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $groups= $this->m_groups->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $groups
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
        log_message('debug', 'Groups.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $groups= $this->m_groups->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $groups
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
        log_message('debug', 'Groups.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('groupsid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_groups->deleteRow($tempId);
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
        $this->load->model('M_groups', 'm_groups');
    }
}
?>