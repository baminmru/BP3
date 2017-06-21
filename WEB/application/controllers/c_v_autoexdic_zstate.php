
    <?php
	 class C_v_autoexdic_zstate extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function newRow() {
            log_message('debug', 'AUTOexDic_zstate.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
            $name  =  $this->input->get_post('name', TRUE);
            $objtype  =  $this->input->get_post('objtype', TRUE);
            $autoexdic_zstate= $this->m_v_autoexdic_zstate->newRow($name,$objtype);
            $return = $autoexdic_zstate;
            print json_encode($return);
    }
    function getRows() {
            log_message('debug', 'AUTOexDic_zstate.getRows post : '.json_encode($this->input->post(NULL, FALSE)));
           $start=$this->input->get('start', FALSE);
           $limit=$this->input->get('limit', FALSE);
       $group = $this->input->get('group', FALSE);  
      $sort = $this->input->get('sort', FALSE);
      if(!$sort && $group) $sort = $group;
      if(!$sort || $group == $sort) 
          {
              $sort = json_decode($sort);
              //$sort[] = array('property'=>'field_name', 'direction'=>'ASC');
              $sort = json_encode($sort);
          }
          $filter = $this->input->get('filter', FALSE);
           $autoexdic_zstate= $this->m_v_autoexdic_zstate->getRowsSL($start,$limit,$sort,$filter);
           print json_encode($autoexdic_zstate);
    }
    function deleteRow() {
        log_message('debug', 'AUTOexDic_zstate.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_v_autoexdic_zstate->deleteRow($tempId);
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
        $this->load->model('M_v_autoexdic_zstate', 'm_v_autoexdic_zstate');
    }
}