<?php
	 class C_bp3doc_field extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3doc_field.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3doc_field.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3doc_fieldid' =>  $this->input->get_post('bp3doc_fieldid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'tabname' =>   $this->input->get_post('tabname', TRUE)
                ,'fieldgroupbox' =>   $this->input->get_post('fieldgroupbox', TRUE)
                ,'allownull' =>   $this->input->get_post('allownull', TRUE)
                ,'fieldtype' =>   $this->input->get_post('fieldtype', TRUE)
                ,'referencetype' =>   $this->input->get_post('referencetype', TRUE)
                ,'datasize' =>   $this->input->get_post('datasize', TRUE)
                ,'reftopart' =>   $this->input->get_post('reftopart', TRUE)
                ,'internalreference' =>   $this->input->get_post('internalreference', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'isautonumber' =>   $this->input->get_post('isautonumber', TRUE)
                ,'isbrief' =>   $this->input->get_post('isbrief', TRUE)
                ,'istabbrief' =>   $this->input->get_post('istabbrief', TRUE)
                ,'thestyle' =>   $this->input->get_post('thestyle', TRUE)
                ,'themask' =>   $this->input->get_post('themask', TRUE)
                ,'shablonbrief' =>   $this->input->get_post('shablonbrief', TRUE)
            );
            $bp3doc_field = $this->m_bp3doc_field->setRow($data);
            print json_encode($bp3doc_field);
    }
    function newRow() {
            log_message('debug', 'bp3doc_field.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3doc_fieldid' =>  $this->input->get_post('bp3doc_fieldid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'tabname' =>   $this->input->get_post('tabname', TRUE)
                ,'fieldgroupbox' =>   $this->input->get_post('fieldgroupbox', TRUE)
                ,'allownull' =>   $this->input->get_post('allownull', TRUE)
                ,'fieldtype' =>   $this->input->get_post('fieldtype', TRUE)
                ,'referencetype' =>   $this->input->get_post('referencetype', TRUE)
                ,'datasize' =>   $this->input->get_post('datasize', TRUE)
                ,'reftopart' =>   $this->input->get_post('reftopart', TRUE)
                ,'internalreference' =>   $this->input->get_post('internalreference', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'isautonumber' =>   $this->input->get_post('isautonumber', TRUE)
                ,'isbrief' =>   $this->input->get_post('isbrief', TRUE)
                ,'istabbrief' =>   $this->input->get_post('istabbrief', TRUE)
                ,'thestyle' =>   $this->input->get_post('thestyle', TRUE)
                ,'themask' =>   $this->input->get_post('themask', TRUE)
                ,'shablonbrief' =>   $this->input->get_post('shablonbrief', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $bp3doc_field= $this->m_bp3doc_field->newRow($instanceid,$parentid,$data);
            $return = $bp3doc_field;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3doc_field.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3doc_field = $this->m_bp3doc_field->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3doc_field
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
            $parentid=$this->input->get('parentid', FALSE);
            if(isset($parentid)){
                if($parentid!=''){
                    $bp3doc_field= $this->m_bp3doc_field->getRowsByParent($parentid,$sort);
                }else{
                    $bp3doc_field= $this->m_bp3doc_field->getRows($sort);
                }
            }else{
              $bp3doc_field= $this->m_bp3doc_field->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3doc_field
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3doc_field.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3doc_field= $this->m_bp3doc_field->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3doc_field
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
        log_message('debug', 'bp3doc_field.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3doc_field= $this->m_bp3doc_field->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3doc_field
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
        log_message('debug', 'bp3doc_field.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3doc_fieldid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3doc_field->deleteRow($tempId);
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
        $this->load->model('M_bp3doc_field', 'm_bp3doc_field');
    }
}
?>