import React, { useState, useEffect } from "react";
import NewScheduleContainer from './schedules/newScheduleContainer/NewScheduleContainer'
import Home from './home/Home'
import Navbar from './shared/navbar/Navbar'
import { Route, BrowserRouter as Router, Switch } from "react-router-dom"
import Sidebar from "./shared/sidebar/Sidebar";
import Callback from './identity/Callback'
import Logout from './identity/Logout'
import { isLoggedIn } from '../services/auth-service';
import AuthGuard from './identity/auth-guard';
import ScheduleContainer from './travel/schedule/schedule-container/ScheduleContainer';
import Account from './account/Account';
import { axiosConfig } from '../services/configs/axios-config';
import { useSelector } from "react-redux";

const App = () => {
    const [isActive, setIsActive] = useState(true);
    const [contentMargin, setContentMargin] = useState(250);
    const user = useSelector(state => state.authState.user);

    const handleOnClick = () => {
        setIsActive(!isActive);
        setContentMargin(isActive ? 80 : 250);
    }

    useEffect(() => {
        axiosConfig(user);
    }, [user])

    return (
        <div>
            <Router>
                <Navbar isActive={isActive} callback={handleOnClick}/>
                <div>
                        <Sidebar isActive={isActive} />
                        <div id="content" style={{marginLeft: contentMargin + 'px', minHeight: 2000 + 'px'}}>
                            <Switch>
                                <Route exact path="/">
                                    <Home />
                                </Route>
                                <Route path="/new">
                                    <AuthGuard component={NewScheduleContainer}/>
                                </Route>
                                <Route path="/sch">
                                    {/* <AuthGuard component={ScheduleContainer}/> */}
                                    <ScheduleContainer />
                                </Route>
                                <Route path="/callback">
                                    <Callback />
                                </Route>
                                <Route path="/account">
                                    <AuthGuard component={Account} path="/account"/>
                                </Route>
                                <Route path="/signout">
                                    <Logout />
                                </Route>
                            </Switch>
                        </div>
                </div>
            </Router>
        </div>
    )
};

export default App;