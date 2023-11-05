extends HBoxContainer

signal pick_color_pressed

@onready var PickColor: ColorPickerButton = $PickColor


func _on_remove_pressed() -> void:
	queue_free()


func _on_pick_color_picker_created() -> void:
	PickColor.get_picker().presets_visible = false
	PickColor.get_picker().color_modes_visible = false
