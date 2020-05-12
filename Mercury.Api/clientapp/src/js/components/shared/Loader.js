import React, { useEffect } from 'react';

const Loader = (props) => {

    useEffect(() => {
        let parentContainer = document.getElementById(props.containerId);

        if (props.isPending){
            let opacityDiv = document.createElement("div");
            opacityDiv.setAttribute("id", "blocked_div");
            opacityDiv.classList.add("blocked")

            parentContainer.insertBefore(opacityDiv, parentContainer.firstChild);
        } 
        else {
            let opacityDiv = document.getElementById("blocked_div");

            if(opacityDiv) {
                parentContainer.removeChild(opacityDiv);
            }
        }
    })

    return (
        <div className={`loader center ${props.isPending ? "" : "hidden"}`}>
            <i className="fa fa-cog fa-spin" />
        </div>
    )
}

export default Loader;