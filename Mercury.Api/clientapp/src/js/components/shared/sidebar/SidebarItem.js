import React, { useState, useEffect } from "react";
import './sidebar.scss'
import { Link, Router, useLocation } from "react-router-dom"
import List from '@material-ui/core/List'
import ListItem from '@material-ui/core/ListItem'
import ListItemText from '@material-ui/core/ListItemText'

function SidebarItem({ name, icon, children, depthStep = 15, depth = 0, isHidden, href, ...rest }) {
    const [isHiddenState, setisHiddenState] = useState(true);

    return (
        <>
            <ListItem style={{display: isHidden ? 'none' : ''}} button  {...rest} 
                component={href ? Link : null}
                to={href ? href : ""}
                onClick={() => setisHiddenState(!isHiddenState)}>
                <ListItemText className={`${icon ? '' : 'sidebar-item-nested'}`}>
                    <div className="flex-sb-row sidebar-item">
                        <span>
                            <i className={icon}></i>
                            {name}
                        </span>
                        <i className={`fas fa-chevron-down 
                            ${Array.isArray(children) ? '' : 'hidden'}
                            ${isHiddenState ? '' : 'fa-rotate-180'}`}>
                        </i>
                    </div>
                </ListItemText>
            </ListItem>
            {Array.isArray(children) ? (
                <List disablePadding dense>
                    {children.map((child) => (
                        <SidebarItem
                            key={child.name}
                            depth={depth + 1}
                            depthStep={depthStep}
                            isHidden={isHiddenState}
                            href={child.href}
                            {...child}
                        />
                    ))}
                </List>
            ) : null}
        </>
    )
  }

export default SidebarItem;