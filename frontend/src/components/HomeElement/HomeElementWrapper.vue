<template>
    <g>
        <home-element v-for="element in homeElementsByFloor"
                      :data="element"
                      :key="element.id"
        />
    </g>
</template>

<script>
    import {mapState} from 'vuex'
    import HomeElement from "./HomeElement";
    export default {
        name: "HomeElementWrapper",
        components: {HomeElement},
        computed: {
            ...mapState('editor', {
                homeElements: 'homeElements',
                selectFloorId: 'selectFloorId'
            }),
            homeElementsByFloor(){
                return this.homeElements.filter(n=>n.level===this.selectFloorId)
            }
        },
    }
</script>

<style scoped>

</style>
