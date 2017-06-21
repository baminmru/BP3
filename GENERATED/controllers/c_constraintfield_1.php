<?php
	 class C_constraintfield_1 extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'CONSTRAINTFIELD_1.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'CONSTRAINTFIELD_1.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'constraintfield_1id' =>  $this->input->get_post('constraintfield_1id', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'thefield' =>   $this->input->get_post('thefield', TRUE)
            );
            $constraintfield_1 = $this->m_constraintfield_1->setRow($data);
            print json_encode($constraintfield_1);
    }
    function newRow() {
            log_message('debug', 'CONSTRAINTFIELD_1.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'constraintfield_1id' =>  $this->input->get_post('constraintfield_1id', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'thefield' =>   $this->input->get_post('thefield', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $constraintfield_1= $this->m_constraintfield_1->newRow($instanceid,$parentid,$data);
            $return = $constraintfield_1;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'CONSTRAINTFIELD_1.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $constraintfield_1 = $this->m_constraintfield_1->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $constraintfield_1
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
                    $constraintfield_1= $this->m_constraintfield_1->getRowsByParent($parentid,$sort);
                }else{
                    $constraintfield_1= $this->m_constraintfield_1->getRows($sort);
                }
            }else{
              $constraintfield_1= $this->m_constraintfield_1->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $constraintfield_1
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'CONSTRAINTFIELD_1.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $constraintfield_1= $this->m_constraintfield_1->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $constraintfield_1
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
        log_message('debug', 'CONSTRAINTFIELD_1.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $constraintfield_1= $this->m_constraintfield_1->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $constraintfield_1
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
        log_message('debug', 'CONSTRAINTFIELD_1.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('constraintfield_1id', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_constraintfield_1->deleteRow($tempId);
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
        $this->load->model('M_constraintfield_1', 'm_constraintfield_1');
    }
}
?>