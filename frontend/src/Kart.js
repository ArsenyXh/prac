import React,{Component} from 'react';
import {variables} from './Variables.js';

export class Kart extends Component{

    constructor(props){
        super(props);

        this.state={
            warehouses:[],
            karts:[],
            modalTitle:"",
            id:0,
            type:"",
            warehouseaddress:""
        }
    }

    refreshList(){
        fetch(variables.API_URL+'Kart')
        .then(response=>response.json())
        .then(data=>{
            this.setState({karts:data});
        });

        fetch(variables.API_URL+'Warehouse')
        .then(response=>response.json())
        .then(data=>{
            this.setState({warehouses:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }
    
    changeT =(e)=>{
        this.setState({type:e.target.value});
    }
    changeW =(e)=>{
        this.setState({warehouseaddress:e.target.value});
    }
    
    addClick() {
        this.setState({ modalTitle: "Add Kart", id: 0, type: "", warehouseaddress: "" });
    }

    editClick(kk){
        this.setState({
            modalTitle:"Edit Kart",
            id:kk.id,
            type:kk.type,
            warehouseaddress:kk.warehouseaddress
        });
    }

    createClick() {
        fetch(variables.API_URL+'Kart', {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            type: this.state.type,
            warehouseaddress: this.state.warehouseaddress
          })
        })
        .then(res => {
          if (res.ok) {
            return res.json();
          } else {
            throw new Error('Failed to create kart');
          }
        })
        .then(result => {
          alert(result);
          this.refreshList();
        })
        .catch(error => {
          alert(error.message);
        });
      }


    updateClick(){
        fetch(variables.API_URL+'Kart',{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                id:this.state.id,
                type:this.state.type,
                warehouseaddress:this.state.warehouseaddress
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }

    deleteClick(id){
        if(window.confirm('Точно удаляем?')){
        fetch(variables.API_URL+'Kart/'+id,{
            method:'DELETE',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
        }
    }

   
    render(){
        const {
            warehouses,
            karts,
            modalTitle,
            id,
            type,
            warehouseaddress
        }=this.state;

        return(
<div>

    <button type="button"
    className="btn btn-primary m-2 float-end"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
    onClick={()=>this.addClick()}>
        Добавить карт
    </button>
    <table className="table table-striped">
    <thead>
    <tr>
        <th>
            Номер карта
        </th>
        <th>
            Тип карта
        </th>
        <th>
            Адрес склада
        </th>
    </tr>
    </thead>
    <tbody>
        {karts.map(kk=>
            <tr key={kk.id}>
                <td>{kk.id}</td>
                <td>{kk.type}</td>
                <td>{kk.warehouseaddress}</td>
                <td>
                <button type="button"
                className="btn btn-light mr-1"
                data-bs-toggle="modal"
                data-bs-target="#exampleModal"
                onClick={()=>this.editClick(kk)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                    <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                    </svg>
                </button>

                <button type="button"
                className="btn btn-light mr-1"
                onClick={()=>this.deleteClick(kk.id)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                    <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                    </svg>
                </button>

                </td>
            </tr>
            )}
    </tbody>
    </table>

<div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
<div className="modal-dialog modal-lg modal-dialog-centered">
<div className="modal-content">
   <div className="modal-header">
       <h5 className="modal-title">{modalTitle}</h5>
       <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
       ></button>
   </div>

   <div className="modal-body">
    <div className="d-flex flex-row bd-highlight mb-3">
     
     <div className="p-2 w-50 bd-highlight">
    
        <div className="input-group mb-3">
            <span className="input-group-text">Тип</span>
            <input type="text" className="form-control"
            value={type}
            onChange={this.changeT}/>
        </div>

        <div className="input-group mb-3">
            <span className="input-group-text">Адрес склада</span>
            <select className="form-select"
            onChange={this.changeW}
            value={warehouseaddress}>
                {warehouses.map(dep=><option key={dep.id}>
                    {dep.address}
                </option>)}
            </select>
        </div>
     </div>
    </div>

    {id==0?
        <button type="button"
        className="btn btn-primary float-start"
        onClick={()=>this.createClick()}
        >Создать</button>
        :null}

        {id!=0?
        <button type="button"
        className="btn btn-primary float-start"
        onClick={()=>this.updateClick()}
        >Изменить</button>
        :null}
   </div>

</div>
</div> 
</div>


</div>
        )
    }
}