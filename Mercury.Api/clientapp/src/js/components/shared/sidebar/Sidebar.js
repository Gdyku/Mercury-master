import React, { useState, useEffect } from "react";
import './sidebar.scss'
import List from '@material-ui/core/List'
import ListItem from '@material-ui/core/ListItem'
import ListItemText from '@material-ui/core/ListItemText'
import { Link, Router, useLocation, Redirect } from "react-router-dom"
import SidebarItem from './SidebarItem'
import {elements} from '../../../constants/sidebar-positions';


const Sidebar = (props) => {
    return (
        <nav id="sidebar" className={`${props.isActive ? "" : "active"}`}>
            <div className="sidebar-header">
                <h3>Manage Your Travel</h3>
                <strong>MR</strong>
            </div>

            <List disablePadding dense>
                {elements.map((elem, index) => (
                    <SidebarItem
                        key={`${elem.name}${index}`}
                        depthStep={elem.depthStep}
                        depth={elem.depth}
                        href={elem.href}
                        isHidden={false}
                        {...elem}
                    />
                ))}
            </List>
            <div>

            </div>
        </nav>
    )
};

export default Sidebar;