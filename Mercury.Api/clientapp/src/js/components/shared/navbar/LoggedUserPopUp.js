import React, {useState}  from "react";
import './navbar.scss';
import Popover from '@material-ui/core/Popover';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { userSettings } from "../../../constants/user-settings";
import { useHistory } from "react-router-dom";

const LoggedUserPopUp = (props) => {
    const [anchorEl, setAnchorEl] = React.useState(null);
    let history = useHistory();

    const handleClick = event => {
      setAnchorEl(event.currentTarget);
    };
  
    const handleClose = () => {
      setAnchorEl(null);
    };

    const open = Boolean(anchorEl);

    const handleOnClick = (href) => {
        setAnchorEl(null);
        history.push(href);
    }

    return (
        <>
            <i className="fas fa-cog" onClick={handleClick}></i>
            <Popover 
                open={open}
                anchorEl={anchorEl}
                onClose={handleClose}
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'center',
                }}
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                >
                <List component="nav" aria-label="main mailbox folders">
                    {userSettings.map(({label, icon, action}) => 
                        <ListItem button onClick={() => handleOnClick(action)} key={label}>
                            <ListItemText>
                                <span>
                                    <i className={`${icon} margin-right`}></i>
                                    {label}
                                </span>
                            </ListItemText>
                        </ListItem>
                    )}
                </List>
            </Popover>
        </>
    )
};

export default LoggedUserPopUp;