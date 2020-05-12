import React  from "react";
import './navbar.scss';
import {isLoggedIn, startAuthentication} from '../../../services/auth-service';
import { useDispatch, useSelector } from "react-redux";
import LoggedUserPopUp from './LoggedUserPopUp'

const Navbar = (props) => {
    const user = useSelector(state => state.authState.user);

    const handleLogin = () => {
        startAuthentication();
    }

    return (
        <nav className="navbar sticky-top navbar-expand-lg">
            <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggler" aria-controls="navbarToggler" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
            </button>

            <div id="sidebar-control" onClick={props.callback}>
                <i className={`fas fa-arrow-left ${props.isActive ? "" : "fa-rotate-180"}`}></i>
            </div>

            <a className="navbar-brand" href="#">Mercury</a>

            <div className="collapse navbar-collapse" id="navbarToggler">
                <div className="navbar-brand mr-auto mt-2 mt-lg-0">
                </div>
                {user && user.profile  
                    ? <div className="logged-account-btns">
                        <span>{user.profile.sub}</span>
                        <LoggedUserPopUp />
                      </div>
                    : <div className="form-inline my-2 my-lg-0 pull-right account-btns">
                        <button className="btn btn-outline-light my-2 my-sm-0" onClick={handleLogin}>Login</button>
                        <button className="btn btn-light my-2 my-sm-0" type="submit">Sign Up</button>
                      </div>}
            </div>
        </nav>
    )
};

export default Navbar;