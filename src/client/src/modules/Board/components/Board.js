import React, { Component } from 'react';
import { StickyContainer } from 'react-sticky';
import _ from "lodash";
import TopPanel from "./TopPanel";
import { UpdateTask, CreateTask } from "../../Task/components";
import Section from "./Section";
import { TASK_STATUSES } from "../../Task/constants"

const styles = {
    boardsContainer: {
        display: "flex",
        flexDirection: "row",
        justifyContent: "space-between",
        padding: "4px"
    }
}

class Board extends Component {
    render() {
        const { id } = this.props.match.params;
        const { isCreating } = this.props;
        return (
            <StickyContainer>
                <TopPanel />
                <div style={styles.boardsContainer}>
                    {_.map(TASK_STATUSES, st => <Section status={st} />)}
                </div>
                
                {id ? <UpdateTask id="modal" todoId={id} /> : null}
                {isCreating ? <CreateTask /> : null}
            </StickyContainer>
        )
    }
}

export default Board;