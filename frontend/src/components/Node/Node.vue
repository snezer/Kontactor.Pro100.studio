<template>
    <g @click="select">
        <circle :cx="data.position.x"
                :cy="data.position.y"
                r="10"
        opacity="0"></circle>
        <foreignObject
                :x="data.position.x-10"
                :y="data.position.y-10"
                height="20" width="20">
            <img :src="require('@/assets/icon/'+iconUrl)">
        </foreignObject>
    </g>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "Node",
        props: {
            data: Object
        },
        data() {
            return {
                title: '',
                description: '',
                id: '',
                position: {
                    x: '',
                    y: '',
                },
                roomId: '',
                floor: '',
                icon: '',
            }
        },
        computed:{
            ...mapState('editor',{
                modeEditor: 'modeEditor',
                selectedNode: 'selectedNode'
            }),
            iconUrl(){
                let path = ''
                switch (this.data.type) {
                    case 'door' : path = 'door.svg'
                        break;
                    case 'stair' : path = 'stair.svg'
                        break;
                    case 'elevator' : path = 'elevator.svg'
                        break;
                }
                return path
            }
        },
        methods:{
            ...mapActions('editor', {
                selectNode: 'select_node'
            }),
            select(e){
                e.preventDefault();
                if (this.modeEditor=='draw'){
                    return;
                }
                if (this.data.newElement) {
                    return;
                }
                if (!this.data.newElement) {

                    this.selectNode(this.data)
                    e.stopPropagation();
                }

            }
        }
    }
</script>

<style scoped>
    .node-badge{
        width: 64px;
        height: 64px;
        margin: -32px -32px;
    }
</style>
