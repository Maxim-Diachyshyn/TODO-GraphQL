import React from 'react';
import _ from "lodash";
import { Query } from "react-apollo";
import { Select, MenuItem, Avatar, Typography } from '@material-ui/core';
import { PersonAdd as PersonAddIcon } from "@material-ui/icons";
import { usersQuery } from "../queries";
import { userAdded } from "../subscriptions";

const texts = {
    unassigned: "Unassigned"
}

const keys = {
    unassigned: "unassigned"
};

const styles = {
    container: {
        // width: "100%"
        // minWidth: 250,
    },
    item: {
        display: "grid",
        gridTemplateColumns: "auto 1fr",
        gridColumnGap: 8,
        alignItems: "center"
    }
}

const AssignedUserInput = props => {
    const { loading, users, assignedUser } = props;

    const onChange = e => {
        const { value } = e.target;
        if (value === keys.unassigned) {
            props.onChange({ 
                assignedUser: null 
            });
        }
        else {
            props.onChange({ 
                assignedUser: {
                    id: value
                }
            });
        }
    }
    
    return (
        <Select style={styles.container} value={_.get(assignedUser, "id", keys.unassigned)} disabled={loading} onChange={onChange}>
            <MenuItem style={styles.select} key={keys.unassigned} value={keys.unassigned}>
                <div style={styles.item}>
                    <Avatar>
                        <PersonAddIcon/>
                    </Avatar>
                    <Typography variant="button">{texts.unassigned}</Typography>
                </div>
            </MenuItem>
            {_.map(users, u => (
                <MenuItem style={styles.select} key={u.id} value={u.id}>
                    <div style={styles.item}>
                        <Avatar src={u.picture} />
                        <Typography variant="button">{u.username}</Typography>
                    </div>
                </MenuItem>)
            )}
        </Select>    
    );                               
}

let subscription = null;
export default props => {
    return (
        <Query query={usersQuery}>
        {({ loading, error, data, subscribeToMore }) => {
            if (!subscription) {
                subscription = () => subscribeToMore({
                    document: userAdded,
                    updateQuery: (prev, { subscriptionData }) => {
                        if (!subscriptionData.data) return prev;
                        const { userAdded } = subscriptionData.data;
                        const users = _.reject(prev.users, x => x.id === userAdded.id);
                        return {
                            ...prev,
                            users: [...users, userAdded]
                        }
                    },
                });
                subscription();
            }
            return (
                <AssignedUserInput {...props} loading={loading} users={_.get(data, "users", [])}/>
            )
        }}</Query>
    )
}