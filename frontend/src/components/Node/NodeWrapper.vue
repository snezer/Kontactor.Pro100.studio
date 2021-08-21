<template>
    <g>
        <node
                v-for="node in nodeElementsByFloor"
                :data="node"
                :key="node.id"
        />
        {{nodeElements}}
    </g>
</template>

<script>
    import { mapState, mapActions} from 'vuex'
    import Node from './Node'
    export default {
        name: "NodeWrapper",
        components: {
            Node
        },
        computed:{
            ...mapState('editor', {
                nodeElements: 'nodeElements',
                selectFloorId: 'selectFloorId'
            }),
            nodeElementsByFloor(){
                return this.nodeElements
            }
        },
        methods:{
            ...mapActions('editor', {
                getAllNodes: 'get_all_nodes'
            })
        },
        mounted() {
            this.getAllNodes()
        }
    }
</script>

<style scoped>

</style>
