shader_type canvas_item;

uniform float brightness: hint_range(-1.0, 1.0) = 0.0;

void fragment() {
	vec3 rgb = COLOR.rgb;
	
	float new_r = rgb.r + brightness;
	float new_g = rgb.g + brightness;
	float new_b = rgb.b + brightness;
	
	vec3 new_rgb = vec3(new_r, new_g, new_b);
	COLOR.rgb = new_rgb;
}