<template>
    <g @click="select">
        <room  v-if="data.type=='room'" :data="data"/>
        <wall v-else  :data="data" />
    </g>

</template>

<script>
    import Wall from "./Wall";
    import Room from "./Room"
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "HomeElement",
        components: {Wall, Room},
        props: {
            data: Object
        },
        computed:{
            ...mapState('editor',{
                modeEditor: 'modeEditor',
                selectedHomeElement: 'selectedHomeElement'
            }),
        },
        methods:{
            ...mapActions('editor', {
                selectHomeElement: 'select_home_element',
                selectElementForSearche: 'select_home_element_for_searche',
            }),
            select(e){
                e.preventDefault();
                if (this.modeEditor=='draw'){
                    return;
                }
                if (this.modeEditor=='search'){
                    this.selectElementForSearche(this.data)
                    e.stopPropagation();
                    return;
                }
                if (this.data.newElement) {
                    return;
                }
                if (!this.data.newElement) {

                    this.selectHomeElement(this.data)
                    e.stopPropagation();
                }

            }
        }

    }
</script>

<style scoped>

</style>
