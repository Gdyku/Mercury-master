import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { addSchedulePending } from "../../../actions/index";
import Loader from "../../shared/Loader";
import ErrorMessage from "../../shared/ErrorMessage";
import "./ScheduleForm.scss"

const Form = (props) => {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const isPending = useSelector(state => state.scheduleState.isPending);
    const isError = useSelector(state => state.scheduleState.isError);
    const errorMessage = useSelector(state => state.scheduleState.errorMessage);

    const dispatch = useDispatch();

    function handleSubmit(event) {
        event.preventDefault();
        dispatch(addSchedulePending({ name, description, categoryId: 0 }));

        if (props.callback) {
            props.callback();
        }
    }    

    return (
        <form id="ScheduleForm" onSubmit={handleSubmit}>
            <ErrorMessage isError={isError} errorMessage={errorMessage}/>
            <Loader isPending={isPending} containerId="ScheduleForm" />
            <div className="form-group">
                <label htmlFor="title">Title</label>
                <input name="name" className="form-control" placeholder="Title" type="text" id="title" 
                    value={name} onChange={e => setName(e.target.value)}/>
            </div>
            <div className="form-group">
                <label htmlFor="category">Category</label>
                <select name="categoryId" id="category" className="form-control">
                    <option value="1">One</option>
                    <option value="2">Two</option>
                    <option value="3">Three</option>
                </select>
            </div>
            <div className="form-group">
                <label htmlFor="description">Description</label>
                <textarea name="description" type="text" id="description" className="form-control" placeholder="Description"
                     value={description} onChange={e => setDescription(e.target.value)}/>
            </div>
            <div>
                <button className="btn btn-primary">End</button>
                <button className="btn btn-primary" type="submit">Save</button>
            </div>
        </form>
    );
}

export default Form;