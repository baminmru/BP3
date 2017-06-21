<?php
	 class C_bp3card_part extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3card_part.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3card_part.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3card_partid' =>  $this->input->get_post('bp3card_partid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'struct' =>   $this->input->get_post('struct', TRUE)
                ,'allowadd' =>   $this->input->get_post('allowadd', TRUE)
                ,'allowdelete' =>   $this->input->get_post('allowdelete', TRUE)
                ,'allowread' =>   $this->input->get_post('allowread', TRUE)
                ,'allowedit' =>   $this->input->get_post('allowedit', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
            );
            $bp3card_part = $this->m_bp3card_part->setRow($data);
            print json_encode($bp3card_part);
    }
    function newRow() {
            log_message('debug', 'bp3card_part.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3card_partid' =>  $this->input->get_post('bp3card_partid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'struct' =>   $this->input->get_post('struct', TRUE)
                ,'allowadd' =>   $this->input->get_post('allowadd', TRUE)
                ,'allowdelete' =>   $this->input->get_post('allowdelete', TRUE)
                ,'allowread' =>   $this->input->get_post('allowread', TRUE)
                ,'allowedit' =>   $this->input->get_post('allowedit', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3card_part= $this->m_bp3card_part->newRow($instanceid,$data);
            $return = $bp3card_part;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3card_part.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3card_part = $this->m_bp3card_part->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3card_part
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
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3card_part= $this->m_bp3card_part->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3card_part= $this->m_bp3card_part->getRows($sort);
                }
            }else{
              $bp3card_part= $this->m_bp3card_part->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3card_part
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3card_part.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3card_part= $this->m_bp3card_part->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3card_part
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
        log_message('debug', 'bp3card_part.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3card_part= $this->m_bp3card_part->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3card_part
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
        log_message('debug', 'bp3card_part.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3card_partid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3card_part->deleteRow($tempId);
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
        $this->load->model('M_bp3card_part', 'm_bp3card_part');
    }
}
?>