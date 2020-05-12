import React, { Component } from "react";
import { startAuthentication } from '../../services/auth-service';
import { Route } from "react-router-dom"
import { useSelector } from "react-redux";

const AuthGuard = ({component: Component, path, ...rest}) => {
    const user = useSelector(state => state.authState.user);

    if(!user) {
        localStorage.setItem("path", "/account");
        startAuthentication();
    } else {
        console.log(user.expires_at)
    }

    return (
        <Route
            {...rest}
            render={ props => <Component {...props} /> }
        />
    );
};

export default AuthGuard;