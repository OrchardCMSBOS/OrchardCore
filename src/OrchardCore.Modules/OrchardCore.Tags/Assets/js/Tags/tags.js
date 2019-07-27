var tagsArray = [];

function initializeTagsEditor(elementId, selectedTags, globalTags, multiple) {

    var vueMultiselect = Vue.component('vue-multiselect', window.VueMultiselect.default);

    var vm = new Vue({
        el: '#' + elementId,
        components: { 'vue-multiselect': vueMultiselect },
        data: {
            arrayOfItems: selectedTags,
            options: globalTags
        },
        computed: {
            selectedIds: function () {
                return this.arrayOfItems.map(function (x) { return x }).join(',');
            },
            isDisabled: function () {
                return this.arrayOfItems.length > 0 && !multiple;
            }
        },
        created: function () {
            var self = this;
        },
        methods: {
            addTag(newTag) {
                this.arrayOfItems.push(newTag);
            }
        }
    });

    return vm;
}
