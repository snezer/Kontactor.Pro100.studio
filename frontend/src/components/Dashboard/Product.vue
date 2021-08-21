<template>
  <div class="product-wrapper">
    <div class="product-filter">
      <div class="search">
        <div style="height: 48px; width: 96%;">
          <v-text-field solo flat append-icon="mdi-magnify" label="Поиск"></v-text-field>
        </div>
      </div>
      <div class="filter">
        <v-card flat>
          <v-card-title>Фильтры</v-card-title>
          <v-card-text>
            <h2 class="text-h6 mb-2">
              Choose amenities
            </h2>

            <v-chip-group
                v-model="amenities"
                column
                multiple
            >
              <v-chip
                  filter
                  outlined
              >
                Elevator
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Washer / Dryer
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Fireplace
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Wheelchair access
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Dogs ok
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Cats ok
              </v-chip>
            </v-chip-group>
          </v-card-text>

          <v-card-text>
            <h2 class="text-h6 mb-2">
              Choose neighborhoods
            </h2>

            <v-chip-group
                v-model="neighborhoods"
                column
                multiple
            >
              <v-chip
                  filter
                  outlined
              >
                Snowy Rock Place
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Honeylane Circle
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Donna Drive
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Elaine Street
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Court Street
              </v-chip>
              <v-chip
                  filter
                  outlined
              >
                Kennedy Park
              </v-chip>
            </v-chip-group>
          </v-card-text>
        </v-card>
      </div>
    </div>
    <div class="product-list-wrapper">
      <div class="product-list">
        <v-card class="elevation-1" width="300" height="380" v-for="i in 5" :key="i">
          <v-img
              height="250"
              src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
          >
            <v-btn icon style="position: absolute;  bottom: 0; right: 0">
              <v-icon color="white">
                mdi-pencil
              </v-icon>
            </v-btn>
          </v-img>

          <v-card-title>Продукт 1</v-card-title>

          <v-card-text>
            <v-row
                align="center"
                class="mx-0"
            >
              <v-rating
                  :value="4.5"
                  color="amber"
                  dense
                  half-increments
                  readonly
                  size="14"
              ></v-rating>

              <div class="grey--text ms-4">
                4.5 (413)
              </div>
            </v-row>
            <div class="my-4 font-weight-black" style="font-size: 1.3em; padding-top: 10px">
              30 руб
            </div>
          </v-card-text>
        </v-card>
        <v-card class="elevation-1 grey lighten-3" width="300" height="380" flat style="display: flex; justify-content: center; align-items: center">
          <v-btn icon style="background-color: #eee">
            <v-icon color="white" large>
              mdi-plus
            </v-icon>
          </v-btn>
        </v-card>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Product"
}
</script>

<style scoped lang="sass">
.product-wrapper
  width: 100%
  height: 100%
  display: grid
  grid-template-columns: 3fr 10fr
.product-filter
  position: relative
  display: grid
  grid-template-rows: 75px 1fr
.search
  display: flex
  justify-content: center
  align-items: center
.product-filter::after
  position: absolute
  top: 3px
  right: 3px
  width: 1px
  height: 99%
  background-color: #eee
  content: ''
.product-list-wrapper
  width: 100%
  display: flex
  justify-content: center
  align-items: start
.product-list
  width: 100%
  display: flex
  justify-content: start
  align-items: start
  padding: 2%
  flex-wrap: wrap
  height: 100%
  overflow-y: auto
  gap: 10px
</style>