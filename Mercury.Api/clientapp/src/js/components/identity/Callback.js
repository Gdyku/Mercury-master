import React, { useEffect, useState } from "react";
import { completeAuthentication } from '../../services/auth-service'
import { useDispatch, useSelector } from "react-redux";
import { logInSuccess } from '../../actions/index';
import {  Redirect } from "react-router-dom";

const Callback = () => {
    const dispatch = useDispatch();

    const currentPath = localStorage.getItem("path");

    const [path, setPath] = useState(currentPath);
    const [loggedUser, setloggedUser] = useState(null);
    const [canRedirect, setCanRedirect] = useState(false);

    const getUserData = async() => {
        const user = await completeAuthentication();

        if(user) {
            dispatch(logInSuccess(user));
            setloggedUser(user);
        }
    }

    useEffect(() => {
        getUserData(); 
        
        if(path && loggedUser) {
            localStorage.removeItem("path");
            setCanRedirect(true)
        }
    }, [path, loggedUser]);

    return (
        <div>
            {path}
            {canRedirect ? <Redirect to={path} /> : null}
        </div>
      )
}

export default Callback;