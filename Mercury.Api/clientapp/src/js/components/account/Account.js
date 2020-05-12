import React, { useState, useEffect } from 'react';
import TextField from '@material-ui/core/TextField';
import Switch from '@material-ui/core/Switch';
import Button from '@material-ui/core/Button';
import './Account.scss';
import { useDispatch, useSelector } from "react-redux";
import { fetchUserPending, updateUserPending } from '../../actions/index';

const Account = () => {
    const dispatch = useDispatch();
    const user = useSelector(state => state.authState.user);
    const userSelector = useSelector(state => state.userState);

    const [isInputHidden, setIsInputHidden] = useState(true);
    const [state, setState] = useState({
        email: '',
        isNewsletterEnabled: false,
        isRecommendedReadingEnabled: false
    });

    useEffect(() => {
        if(!userSelector.isPending && !userSelector.errorMessage) {
            if(!userSelector.user) {
                dispatch(fetchUserPending(user));
            } 
            else {
                setState(userSelector.user);
            }
        }
    }, [userSelector])

    const handleChange = event => {
        dispatch(updateUserPending({ ...state, [event.target.name]: event.target.checked }));
    };

    return (
        <section id="account">
            <h2>User settings</h2>

            <article className="d-flex justify-content-between">
                <div>
                    <h4>Your email</h4>

                    {isInputHidden
                        ? <p>{state.email}</p>
                        : <TextField defaultValue={state.email} name="email" />
                    }
                </div>
                <Button variant="outlined" style={{height: 36 + 'px'}} onClick={() => setIsInputHidden(!isInputHidden)}>Edit email</Button>
            </article>

            <article>
                <h4 className="mb-4">Emails from Mercury</h4>

                <div className="d-flex justify-content-between mb-4">
                    <div>
                        <h5>Medium Digest</h5>
                        <p>The best stories on Medium personalized based on your<br/> interests, as well as outstanding stories selected by our editors</p>
                    </div>

                    <Switch
                        checked={state.isNewsletterEnabled}
                        onChange={handleChange}
                        name="isNewsletterEnabled"
                        color="primary"
                    />
                </div>

                 <div className="d-flex justify-content-between">
                    <div>
                        <h5>Recommended reading</h5>
                        <p>Featured stories, columns, and collections that we think you’ll<br/> enjoy based on your reading history</p>
                    </div>

                    <Switch
                        checked={state.isRecommendedReadingEnabled}
                        onChange={handleChange}
                        name="isRecommendedReadingEnabled"
                        color="primary"
                    />
                </div>
            </article>

            <article>
                <h4 className="mb-4">Security</h4>

                <div className="d-flex justify-content-between mb-4">
                    <div>
                        <h5>Change password</h5>
                        <p>The best stories on Medium personalized based on your<br/> interests, as well as outstanding stories selected by our editors</p>
                    </div>

                    <Button variant="outlined" style={{height: 36 + 'px'}}>Change</Button>
                </div>

                 <div className="d-flex justify-content-between">
                    <div>
                        <h5>Delete account</h5>
                        <p>Permanently delete your account and all of your content.</p>
                    </div>

                    <Button variant="contained" color="secondary" style={{height: 36 + 'px'}}>
                        Delete
                    </Button>
                </div>
            </article>
        </section>
    );
}

export default Account;